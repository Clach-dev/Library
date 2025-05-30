using System.Security.Cryptography;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Domain.Interfaces.IRepositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.Data.Repositories;

public abstract class BlobStorageRepository : IStorageRepository
{
    protected readonly BlobServiceClient BlobServiceClient;
    protected readonly string ContainerName;

    protected BlobStorageRepository(BlobServiceClient blobServiceClient, string containerName)
    {
        BlobServiceClient = blobServiceClient;
        ContainerName = containerName;
    }

    public async Task<Uri> UploadFileAsync(Stream fileStream, CancellationToken cancellationToken = default)
    {
        var containerClient = await EnsureContainerExistsAsync(cancellationToken);

        using var image = await Image.LoadAsync(fileStream, cancellationToken);
        image.Mutate(x => x.AutoOrient());

        var (encodedStream, encoder, extension) = await EncodeImageAsync(image, cancellationToken);
        var hash = ComputeSHA256(encodedStream);

        var blobName = $"{hash}.{extension}";
        var blobClient = containerClient.GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync(cancellationToken))
        {
            encodedStream.Position = 0;
            await blobClient.UploadAsync(encodedStream, false, cancellationToken);
        }

        return blobClient.Uri;
    }

    public async Task DeleteFileAsync(Uri imageUri, CancellationToken cancellationToken = default)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);
        var fileName = imageUri.Segments[^1];
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Uri>> GetReadOnlyImageUrisAsync(IEnumerable<Uri> imageUris)
    {
        var imageSafeUris = new List<Uri>();
        var containerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);

        foreach (var imageUri in imageUris)
        {
            if (!imageUri.IsAbsoluteUri || imageUri.Segments.Length == 0)
                continue;

            var fileName = imageUri.Segments[^1];
            var blobClient = containerClient.GetBlobClient(fileName);

            if (blobClient.CanGenerateSasUri)
            {
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = ContainerName,
                    BlobName = blobClient.Name,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(60)
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                imageSafeUris.Add(blobClient.GenerateSasUri(sasBuilder));
            }
            else
            {
                throw new InvalidOperationException("Blob client does not support SAS URI generation.");
            }
        }

        return await Task.FromResult(imageSafeUris);
    }
    
    private async Task<BlobContainerClient> EnsureContainerExistsAsync(CancellationToken cancellationToken)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(ContainerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        return containerClient;
    }

    private static async Task<(MemoryStream Stream, IImageEncoder Encoder, string Extension)> EncodeImageAsync(
        Image image,
        CancellationToken cancellationToken)
    {
        var format = image.Metadata.DecodedImageFormat 
                     ?? throw new InvalidOperationException("Image format could not be determined.");
    
        IImageEncoder encoder = format.DefaultMimeType switch
        {
            "image/png" => new PngEncoder(),
            _ => new JpegEncoder { Quality = 85 }
        };
    
        var extension = format.DefaultMimeType switch
        {
            "image/png" => "png",
            _ => "jpg"
        };
    
        var outputStream = new MemoryStream();
        await image.SaveAsync(outputStream, encoder, cancellationToken);
        outputStream.Position = 0;
    
        return (outputStream, encoder, extension);
    }

    private static string ComputeSHA256(Stream stream)
    {
        stream.Position = 0;
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}

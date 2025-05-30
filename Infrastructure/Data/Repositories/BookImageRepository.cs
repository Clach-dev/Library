using Domain.Interfaces.IRepositories;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Repositories;

public class BookImageRepository(BlobServiceClient blobServiceClient, IConfiguration configuration)
    : BlobStorageRepository(blobServiceClient, configuration.GetValue<string>("ContainerNameStrings:BookImageContainer")!),
        IBookImageRepository
{
}
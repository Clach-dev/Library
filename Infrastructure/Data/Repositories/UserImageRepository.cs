using Azure.Storage.Blobs;
using Domain.Interfaces.IRepositories;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Repositories;

public class UserImageRepository(BlobServiceClient blobServiceClient, IConfiguration configuration)
    : BlobStorageRepository(blobServiceClient, configuration.GetValue<string>("ContainerNameStrings:UserImageContainer")!),
        IUserImageRepository
{
}
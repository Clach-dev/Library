namespace Domain.Interfaces.IRepositories;

public interface IStorageRepository
{
    Task<Uri> UploadFileAsync(Stream fileStream, CancellationToken cancellationToken = default);
    
    Task DeleteFileAsync(Uri uri, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Uri>> GetReadOnlyImageUrisAsync(IEnumerable<Uri> imageUris); 
}   
namespace FileDemo.Api.Services
{
    public interface IBlobService
    {
        Task<Guid> UploadAsync(Stream stream, string ContentType, CancellationToken cancellationToken = default);

        Task<FileResponse> DownloadAsync(Guid filedId, CancellationToken cancellationToken);

        Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
    }
}

public record FileResponse(Stream Stream, string ContentType);
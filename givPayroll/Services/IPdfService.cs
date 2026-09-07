namespace givPayroll.Services;

public interface IPdfService
{
    Task<byte[]> GeneratePdfFromUrlAsync(
        string url,
        CancellationToken cancellationToken = default);
}
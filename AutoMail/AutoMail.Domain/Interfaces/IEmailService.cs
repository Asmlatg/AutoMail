namespace AutoMail.Domain.Interfaces;

public interface IEmailService
{
    Task SendReceiptToHrAsync(string hrEmail, string fileName, Stream fileStream, CancellationToken cancellationToken);
}
namespace AutoMail.Infrastructure.Configuration;

public class SmtpOptions
{
    public required string Server { get; init; }
    public required int Port { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public bool EnableSsl { get; init; } = true;
    public required string FromAddress { get; init; }
    public required string FromName { get; init; }
}
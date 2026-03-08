using System.Net.Mail;
using System.Net.Mime;
using AutoMail.Domain.Interfaces;
using AutoMail.Infrastructure.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ContentType = MimeKit.ContentType;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AutoMail.Infrastructure.Services;

public class SmtpEmailService(
    IOptions<SmtpOptions> options,
    ILogger<SmtpEmailService> logger) : IEmailService
{
    private readonly SmtpOptions _smtpOptions = options.Value;

    public async Task SendReceiptToHrAsync(string hrEmail, string fileName, Stream fileStream, CancellationToken cancellationToken)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromAddress));
            message.To.Add(new MailboxAddress("HR Department", hrEmail));
            message.Subject = $"Transport Receipt Submission: {fileName}";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = "Please find the attached transport receipt for processing."
            };

            // Stream is passed directly to the attachment builder to avoid loading the whole file into RAM
            await bodyBuilder.Attachments.AddAsync(fileName, fileStream, ContentType.Parse("application/pdf"), cancellationToken);
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            
            await client.ConnectAsync(_smtpOptions.Server, _smtpOptions.Port, 
                _smtpOptions.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto, cancellationToken);
            
            if (!string.IsNullOrEmpty(_smtpOptions.Username))
            {
                await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password, cancellationToken);
            }

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            logger.LogInformation("Successfully sent receipt {FileName} to {HrEmail}", fileName, hrEmail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send receipt {FileName} to {HrEmail}", fileName, hrEmail);
            throw;
        }
    }
}
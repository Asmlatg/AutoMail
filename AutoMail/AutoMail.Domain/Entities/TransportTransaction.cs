using AutoMail.Domain.Common;
using AutoMail.Domain.Entities.enums;

namespace AutoMail.Domain.Entities;

public class TransportTransaction : Entity<Guid>
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public DateTimeOffset TransactionTime { get; init; } = DateTimeOffset.UtcNow;
    public ReceiptReceptionState Status { get; set; } = ReceiptReceptionState.Pending;
    public string? ErrorMessage { get; set; }
}
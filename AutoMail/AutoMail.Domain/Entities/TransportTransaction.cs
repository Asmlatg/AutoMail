using AutoMail.Domain.Common;

namespace AutoMail.Domain.Entities;

public class TransportTransaction : Entity<Guid>
{
    public DateTimeOffset TrasactionTime { get; set; }
}
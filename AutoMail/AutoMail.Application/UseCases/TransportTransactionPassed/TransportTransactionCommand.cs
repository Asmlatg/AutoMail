using MediatR;

namespace AutoMail.Application.UseCases.TransportTransactionPassed;

public record CreateTransportTransactionCommand(string Name, decimal Price) : IRequest<Guid>;
using MediatR;

namespace AutoMail.Application.UseCases.TransportTransactionPassed;

public record CreateTransportTransactionCommand(
    Stream FileStream, 
    string FileName, 
    string HrEmail) : IRequest<Guid>;
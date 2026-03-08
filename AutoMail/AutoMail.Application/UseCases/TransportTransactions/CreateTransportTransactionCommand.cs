using MediatR;

namespace AutoMail.Application.UseCases.TransportTransactions;

public record CreateTransportTransactionCommand(
    Stream FileStream, 
    string FileName, 
    string HrEmail) : IRequest<Guid>;
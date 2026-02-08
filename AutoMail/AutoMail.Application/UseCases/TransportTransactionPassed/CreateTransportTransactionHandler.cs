using AutoMail.Domain.Interfaces;
using MediatR;

namespace AutoMail.Application.UseCases.TransportTransactionPassed;

public class CreateTransportTransactionHandler : IRequestHandler<CreateTransportTransactionCommand, Guid>
{
    private readonly ITransactionRepository _repository;

    public CreateTransportTransactionHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }
    public Task<Guid> Handle(CreateTransportTransactionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using AutoMail.Domain.Entities;
using AutoMail.Domain.Interfaces;

namespace AutoMail.Infrastructure.Persistence.Repositories;

public class TransportTransactionRepository : ITransactionRepository
{
    public Task<List<TransportTransaction?>> GetAllTransportTransactionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(TransportTransaction product)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
using AutoMail.Domain.Entities;

namespace AutoMail.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<List<TransportTransaction?>> GetAllTransportTransactionsAsync();
    Task AddAsync(TransportTransaction product);
    Task SaveChangesAsync();
}
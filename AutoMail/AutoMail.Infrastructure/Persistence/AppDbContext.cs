using AutoMail.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMail.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<TransportTransaction> Products { get; set; }
}
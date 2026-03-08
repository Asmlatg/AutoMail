using AutoMail.Application.Configurations;
using AutoMail.Domain.Interfaces;
using AutoMail.Infrastructure.Configuration;
using AutoMail.Infrastructure.Persistence;
using AutoMail.Infrastructure.Persistence.Repositories;
using AutoMail.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMail.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<SmtpOptions>(config.GetSection(SectionConfiguration.SmtpSettings));
        
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<ITransactionRepository, TransportTransactionRepository>();
        services.AddScoped<IEmailService, SmtpEmailService>();

        return services;
    }
}
using AutoMail.Application.UseCases.TransportTransactionPassed;
using AutoMail.Domain.Entities;
using AutoMail.Domain.Entities.enums;
using AutoMail.Domain.Interfaces;
using MediatR;

namespace AutoMail.Application.UseCases.TransportTransactions;

public class CreateTransportTransactionCommandHandler(
    ITransactionRepository repository,
    IEmailService emailService) : IRequestHandler<CreateTransportTransactionCommand, Guid>
{
    public async Task<Guid> Handle(CreateTransportTransactionCommand request, CancellationToken cancellationToken)
    {
        var receipt = new TransportTransaction 
        { 
            FileName = request.FileName,
            Id = Guid.NewGuid() // Or let the DB generate it
        };
        
        await repository.AddAsync(receipt, cancellationToken);
        
        try
        {
            // Stream the file directly to the email service
            await emailService.SendReceiptToHrAsync(request.HrEmail, request.FileName, request.FileStream, cancellationToken);
            receipt.Status = ReceiptReceptionState.Sent;
        }
        catch (Exception ex)
        {
            receipt.Status = ReceiptReceptionState.Failed;
            receipt.ErrorMessage = ex.Message;
        }
        finally
        {
            await repository.SaveChangesAsync(cancellationToken);
        }

        return receipt.Id;
    }
}
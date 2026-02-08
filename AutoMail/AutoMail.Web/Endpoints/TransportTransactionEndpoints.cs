using AutoMail.Application.UseCases.TransportTransactionPassed;
using MediatR;

namespace AutoMail.Endpoints;

public class TransportTransactionEndpoints
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/TransportTransactions");

        group.MapPost("/", CreateTransportTransaction);
    }

    private static async Task<IResult> CreateTransportTransaction(ISender sender,
        CreateTransportTransactionCommand command)
    {
        var transportTransactionId = await sender.Send(command);
        return Results.Created($"/api/TransportTransactions/{transportTransactionId}", transportTransactionId);
    }
}
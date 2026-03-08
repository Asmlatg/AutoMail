using AutoMail.Application.UseCases.TransportTransactions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AutoMail.Web.Endpoints;

public class TransportTransactionEndpoints
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/TransportTransactions");
        
        group.MapPost("/", CreateTransportTransaction)
            .DisableAntiforgery();
    }

    private static async Task<IResult> CreateTransportTransaction(
        IFormFile file,
        [FromForm] string hrEmail,
        ISender sender,
        CancellationToken ct)
    {
        if (file is null || file.Length == 0) return Results.BadRequest("No file was uploaded.");
        if (string.IsNullOrWhiteSpace(hrEmail)) return Results.BadRequest("HR Email is required.");
        
        await using var stream = file.OpenReadStream();
        
        var command = new CreateTransportTransactionCommand(stream, file.FileName, hrEmail);
        
        var transportTransactionId = await sender.Send(command, ct);
        
        return Results.Created($"/api/TransportTransactions/{transportTransactionId}", transportTransactionId);
    }
}
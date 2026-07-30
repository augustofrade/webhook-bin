using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebhookBin.App.Features.Bins;


public static class CreateBin
{
    public record Request(string BinName);
    public record Response(string BinName);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.BinName).NotNull().NotEmpty();
        }
    } 
    
    public sealed class Endpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost("/", Handler);
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler(Request request, IValidator<Request> validator)
    {
        return TypedResults.Ok(new Response(request.BinName)); 
    }
}
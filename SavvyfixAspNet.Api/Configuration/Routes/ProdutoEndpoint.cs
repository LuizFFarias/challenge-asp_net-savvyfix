using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class ProdutoEndpoint
{
    public static void MapProdutoEndpoints(this WebApplication app)
    {
        var produtosGroup = app.MapGroup("/produtos");

        produtosGroup.MapGet("/", async (SavvyfixMetadataDbContext dbcontext) =>
            {
                var produtos = await dbcontext.Produtos.ToListAsync();
                return produtos.Any() ? Results.Ok(produtos) : Results.NotFound();
            })
            .WithName("Buscar produtos")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetProdutos",
                Summary = "Retorna todos os produtos",
                Description = "Retorna todos os produtos",
                Deprecated = false
            })
            .Produces<List<Produto>>()
            .Produces(StatusCodes.Status404NotFound);



    }
    
}
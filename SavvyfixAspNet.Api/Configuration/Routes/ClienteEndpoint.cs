using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class ClienteEndpoint
{
    public static void MapClienteEndpoints(this WebApplication app)
    {
         var clientesGroup = app.MapGroup("/clientes");

            // GET: Retorna todos os clientes
            clientesGroup.MapGet("/",  async (SavvyfixMetadataDbContext dbContext) =>
            {
                var clientes = await dbContext.Clientes.Include(c => c.Endereco).ToListAsync();
                return clientes.Any() ? Results.Ok(clientes) : Results.NotFound();
            }).RequireAuthorization()
            .WithName("Buscar clientes")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetClientes",
                Summary = "Retorna todos os clientes",
                Description = "Retorna todos os clientes do banco de dados. **É necessário estar autenticado**",
                Deprecated = false
            })
            .Produces<List<Cliente>>()
            .Produces(StatusCodes.Status404NotFound);

            // GET: Retorna um cliente pelo ID
            clientesGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var cliente = await dbContext.Clientes
                    .Include(c => c.Endereco)
                    .Include(c => c.Compras)
                    .Include(c => c.Atividades)
                    .FirstOrDefaultAsync(c => c.IdCliente == id);

                return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
            })
            .RequireAuthorization()
            .WithName("Buscar cliente pelo id")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetClienteById",
                Summary = "Retorna o cliente buscado pelo ID",
                Description = "Retorna o cliente buscado pelo ID do banco de dados. **É necessário estar autenticado**",
                Deprecated = false
            })
            .Produces<Cliente>()
            .Produces(StatusCodes.Status404NotFound);

            // POST: Adiciona um novo cliente
            clientesGroup.MapPost("/", async (ClienteAddOrUpdateModel clienteModel, SavvyfixMetadataDbContext dbContext) =>
                {
                    // Verifica se o modelo é válido
                    if (!clienteModel.IsValid(out var validationErrors))
                    {
                        return Results.BadRequest(validationErrors); // Retorna os erros de validação
                    }
                    
                    Endereco endereco = await dbContext.Enderecos.FindAsync(clienteModel.IdEndereco);
                    if (endereco is null)
                    {
                        return Results.BadRequest("Endereço não encontrado");
                    }
                    
                    var novoCliente = clienteModel.MapToCliente();
                    dbContext.Clientes.Add(novoCliente);
                    await dbContext.SaveChangesAsync();

                    ClienteRole clienteRole = new ClienteRole()
                    {
                        IdCliete = novoCliente.IdCliente,
                        IdRole = 2
                    };
                    
                    dbContext.ClienteRoles.Add(clienteRole);
                    await dbContext.SaveChangesAsync();

                return Results.Created($"/clientes", clienteModel);
            })
            .WithName("Adicionar novo cliente")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "AddCliente",
                Summary = "Adiciona um novo cliente",
                Description = "Adiciona um novo cliente ao banco de dados.",
                Deprecated = false
            })
            .Accepts<ClienteAddOrUpdateModel>("application/json")
            .Produces<Cliente>(StatusCodes.Status201Created);

            // PUT: Atualiza um cliente existente
            clientesGroup.MapPut("/{id:long}", async (long id, ClienteAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
            {
                var existingCliente = await dbContext.Clientes.FindAsync(id);

                if (existingCliente is null)
                {
                    return Results.NotFound();
                }

                existingCliente.CpfClie = updateModel.CpfClie ?? existingCliente.CpfClie;
                existingCliente.NmClie = updateModel.NmClie ?? existingCliente.NmClie;
                existingCliente.SenhaClie = updateModel.SenhaClie ?? existingCliente.SenhaClie;
                existingCliente.IdEndereco = updateModel.IdEndereco ?? existingCliente.IdEndereco;

                await dbContext.SaveChangesAsync();

                return Results.Ok(existingCliente);
            })
            .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
            .WithName("Atualizar cliente")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "UpdateCliente",
                Summary = "Atualiza um cliente existente",
                Description = "Atualiza um cliente existente no banco de dados pelo ID. **É necessário ser um administrador**",
                Deprecated = false
            })
            .Accepts<ClienteAddOrUpdateModel>("application/json")
            .Produces<Cliente>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
            
            
            //PUT: Atualiza ROLE de um cliente existente
            clientesGroup.MapPut("/{id:long}/role", async (long id, ClienteRoleAddOrUpdateModel roleUpdate, SavvyfixMetadataDbContext dbContext) =>
                {
                    var clienteRole = await dbContext.ClienteRoles
                        .FirstOrDefaultAsync(cr => cr.IdCliete == id && cr.IdRole != roleUpdate.IdRole);

                    if (clienteRole == null)
                    {
                        return Results.NotFound("Relacionamento entre Cliente e Role não encontrado ou já é o mesmo.");
                    }
                    
                    dbContext.ClienteRoles.Remove(clienteRole);
                    await dbContext.SaveChangesAsync();
                    
                    var novoClienteRole = new ClienteRole
                    {
                        IdCliete = id,
                        IdRole = roleUpdate.IdRole
                    };
                    dbContext.ClienteRoles.Add(novoClienteRole);
                    
                    await dbContext.SaveChangesAsync();

                    return Results.Ok(novoClienteRole);
                })
                .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
                .WithName("AtualizarClienteRole")
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "UpdateClienteRole",
                    Summary = "Atualiza o relacionamento ClienteRole de um cliente existente",
                    Description = "Atualiza o relacionamento ClienteRole de um cliente existente no banco de dados pelo ID do cliente e novo ID da role. **É necessário ser um administrador**",
                    Deprecated = false
                })
                .Produces<ClienteRole>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);




            // DELETE: Deleta um cliente existente
            clientesGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var cliente = await dbContext.Clientes.FindAsync(id);

                if (cliente is null)
                {
                    return Results.NotFound();
                }

                dbContext.Clientes.Remove(cliente);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            })
            .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
            .WithName("Deletar cliente")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "DeleteCliente",
                Summary = "Deleta um cliente existente",
                Description = "Deleta um cliente existente no banco de dados pelo ID. **É necessário ser um administrador**",
                Deprecated = false
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
            

    }
}
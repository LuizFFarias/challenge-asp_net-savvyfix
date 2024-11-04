using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.ML;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class AtividadesEndpoint
{
    public static void MapAtividadesEndpoints(this WebApplication app)
    {
        var atividadesGroup = app.MapGroup("/atividades");

        // GET: Retorna todas as atividades
        atividadesGroup.MapGet("/", async (SavvyfixMetadataDbContext dbContext) =>
        {
            var atividades = await dbContext.Atividades
                .Include(a => a.Cliente)
                .ToListAsync();
            return atividades.Any() ? Results.Ok(atividades) : Results.NotFound();
        })
        .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
        .WithName("Buscar atividades")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "GetAtividades",
            Summary = "Retorna todas as atividades",
            Description = "Retorna todas as atividades do banco de dados. **É necessário ser um administrador**",
            Deprecated = false
        })
        .Produces<List<Atividades>>()
        .Produces(StatusCodes.Status404NotFound);

        // GET: Retorna uma atividade pelo ID
        atividadesGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
        {
            var atividade = await dbContext.Atividades
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(a => a.IdAtividades == id);

            return atividade is not null ? Results.Ok(atividade) : Results.NotFound();
        })
        .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
        .WithName("Buscar atividade pelo id")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "GetAtividadesById",
            Summary = "Retorna uma atividade pelo ID",
            Description = "Retorna a atividade pelo ID do banco de dados. **É necessário ser um administrador**",
            Deprecated = false
        })
        .Produces<Atividades>()
        .Produces(StatusCodes.Status404NotFound);

        // POST: Adiciona uma nova atividade
        atividadesGroup.MapPost("/", async (AtividadesAddOrUpdateModel atividadeModel, SavvyfixMetadataDbContext dbContext) =>
        {

            // Verifica se o modelo é válido
            if (!atividadeModel.IsValid(out var validationErrors))
            {
                return Results.BadRequest(validationErrors); // Retorna os erros de validação
            }

            Cliente cliente = await dbContext.Clientes.FindAsync(atividadeModel.IdCliente);
            if (cliente is null)
            {
                return Results.BadRequest("Cliente não encontrado");
            }
            
            Produto produto = await dbContext.Produtos.FindAsync(atividadeModel.IdProduto);
            if (produto is null)
            {
                return Results.BadRequest("Produtp não encontrado");
            }
            
            // Criação do contexto ML
            var contexto = new MLContext();

            // Obter a raiz do projeto
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\SavvyfixAspNet.ML\\bin\\Debug\\net8.0"));

            // Definir o caminho para o modelo
            var modeloPath = Path.Combine(projectRoot, "modelo.zip");

            ITransformer modelo;
            try
            {
                using (var stream = new FileStream(modeloPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    modelo = contexto.Model.Load(stream, out var schema);
                }
            }
            catch (FileNotFoundException ex)
            {
                return Results.BadRequest($"Modelo não encontrado: {ex.Message}");
            }
            
            // Criar critérios
            var criterios = new PorcentagemCriterios();

            // Criar um objeto de entrada para a previsão
            var atividadeInput = new AtividadeInputModel
            {
                Nome = cliente.NmClie,
                Produto = produto.NmProd,
                Localizacao = atividadeModel.LocalizacaoAtual,
                Horario = atividadeModel.HorarioAtual.ToString(),
                Clima = atividadeModel.ClimaAtual,
                Procura = atividadeModel.QntdProcura,
                Demanda = atividadeModel.DemandaProduto,
                PrecoBase = (float)produto.PrecoFixo
            };

            // Chamar o método para adicionar porcentagens e calcular o preço final
            var atividadeComPorcentagens = PrevisaoService.AdicionarPorcentagens(atividadeInput, criterios);
            
            atividadeModel.PrecoVariado = (decimal)atividadeComPorcentagens;
            
            dbContext.Atividades.Add(atividadeModel.MapToAtv());
            await dbContext.SaveChangesAsync();

            return Results.Created($"/atividades", atividadeModel);
        })
        .RequireAuthorization()
        .WithName("Adicionar nova atividade")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "AddAtividades",
            Summary = "Adiciona uma nova atividade",
            Description = "Adiciona uma nova atividade ao banco de dados. **É necessário estar autenticado**",
            Deprecated = false
        })
        .Accepts<AtividadesAddOrUpdateModel>("application/json")
        .Produces<Atividades>(StatusCodes.Status201Created);

        // PUT: Atualiza uma atividade existente
        atividadesGroup.MapPut("/{id:long}", async (long id, AtividadesAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
        {
            var existingAtividade = await dbContext.Atividades.FindAsync(id);

            if (existingAtividade is null)
            {
                return Results.NotFound();
            }

            existingAtividade.ClimaAtual = updateModel.ClimaAtual.ToString() ?? existingAtividade.ClimaAtual; ;
            existingAtividade.DemandaProduto = updateModel.DemandaProduto ?? existingAtividade.DemandaProduto;
            existingAtividade.HorarioAtual = updateModel.HorarioAtual ?? existingAtividade.HorarioAtual;
            existingAtividade.LocalizacaoAtual = updateModel.LocalizacaoAtual ?? existingAtividade.LocalizacaoAtual;
            existingAtividade.IdCliente = updateModel.IdCliente ?? existingAtividade.IdCliente;
            if (updateModel.PrecoVariado != 0)
            {
                existingAtividade.PrecoVariado = updateModel.PrecoVariado;
            }

            if (updateModel.QntdProcura != 0)
            {
                existingAtividade.QntdProcura = updateModel.QntdProcura;
            }


            await dbContext.SaveChangesAsync();

            return Results.Ok(existingAtividade);
        })
        .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
        .WithName("Atualizar atividade")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "UpdateAtividades",
            Summary = "Atualiza uma atividade existente",
            Description = "Atualiza uma atividade existente no banco de dados pelo ID. **É necessário ser um administrador**",
            Deprecated = false
        })
        .Accepts<AtividadesAddOrUpdateModel>("application/json")
        .Produces<Atividades>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE: Deleta uma atividade existente
        atividadesGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
        {
            var atividade = await dbContext.Atividades.FindAsync(id);

            if (atividade is null)
            {
                return Results.NotFound();
            }

            dbContext.Atividades.Remove(atividade);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute(){Roles = "ROLE_ADMIN"})
        .WithName("Deletar atividade")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "DeleteAtividades",
            Summary = "Deleta uma atividade existente",
            Description = "Deleta uma atividade existente no banco de dados pelo ID. **É necessário ser um administrador**",
            Deprecated = false
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
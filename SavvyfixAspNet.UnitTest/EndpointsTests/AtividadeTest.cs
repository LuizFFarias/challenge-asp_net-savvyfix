using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavvyfixAspNet.Domain.Entities;
using Xunit;

namespace SavvyfixAspNet.UnitTest;

[Collection("Integration Tests")]
public class AtividadeTest
{
    private readonly HttpClient _client;
    private readonly IConfiguration? _config;
    private readonly TokenService _tokenService;

    public AtividadeTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _config = factory.Services.GetService<IConfiguration>();
        _tokenService = new TokenService(_config);
    }
    
    [Fact]
    public async Task GetAtividades_RetornaListaDeAtividades()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Act
        var response = await _client.GetAsync("/atividades");

        // Assert
        response.EnsureSuccessStatusCode();
        var atividades = await response.Content.ReadFromJsonAsync<IEnumerable<Atividades>>();

        Assert.NotNull(atividades);
        Assert.NotEmpty(atividades);
    }
    
    [Fact]
    public async Task GetAtividadesSemAutorizacao_RetornaUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/atividades");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    
    [Fact]
    public async Task GetAtividadeById_RetornaAtividade_SeAtividadeExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        long atividadeId = 1; 

        // Act
        var response = await _client.GetAsync($"/atividades/{atividadeId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var atividade = await response.Content.ReadFromJsonAsync<Atividades>();

        Assert.NotNull(atividade);
    }

    [Fact]
    public async Task GetAtividadeById_RetornaStatus404_SeAtividadeNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        long atividadeId = 999; // Substitua pelo ID que não existe

        // Act
        var response = await _client.GetAsync($"/atividades/{atividadeId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    
    [Fact]
    public async Task CriaAtividade_RetornaAtividadeCadastrada()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        var novaAtividade = new AtividadesAddOrUpdateModel()
        {
            ClimaAtual = 22,
            DemandaProduto = "Al",
            HorarioAtual = DateTime.Now.ToString(),
            LocalizacaoAtual = "Av Paulista",
            QntdProcura = 12,
            IdCliente = 1,
            IdProduto = 2
        };

        // Act
        var response = await _client.PostAsJsonAsync("/atividades", novaAtividade);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var atividadeCadastrada = await response.Content.ReadFromJsonAsync<Atividades>();
        Assert.NotNull(atividadeCadastrada); // Verifica se a atividade retornada não é nula
        Assert.Equal(novaAtividade.ClimaAtual, atividadeCadastrada.ClimaAtual);
        Assert.Equal(novaAtividade.DemandaProduto, atividadeCadastrada.DemandaProduto);
        Assert.Equal(novaAtividade.HorarioAtual, atividadeCadastrada.HorarioAtual);
        Assert.Equal(novaAtividade.LocalizacaoAtual, atividadeCadastrada.LocalizacaoAtual);
        Assert.Equal(750, atividadeCadastrada.PrecoVariado);
        Assert.Equal(novaAtividade.QntdProcura, atividadeCadastrada.QntdProcura);
        Assert.Equal(novaAtividade.IdCliente, atividadeCadastrada.IdCliente);
    }


    
    [Theory]
    [InlineData(22, "Al", -1.00, "Av Paulista", 12, 1L)] // PrecoVariado negativo
    [InlineData(22, "Al", 233.00, "Av Paulista", 12, null)] // IdCliente nulo
    [InlineData(22, "Al", 233.00, "Av Paulista", 12, 999L)] // IdCliente inexistente
    public async Task CriaAtividade_RetornaErro_SeVariosAtributosInvalidos(
        int climaAtual, 
        string demandaProduto, 
        decimal precoVariado, 
        string localizacaoAtual, 
        int qntdProcura, 
        long? idCliente)
    {
        
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        var novaAtividade = new AtividadesAddOrUpdateModel()
        {
            ClimaAtual = Convert.ToInt32(climaAtual),
            DemandaProduto = demandaProduto,
            PrecoVariado = precoVariado,
            LocalizacaoAtual = localizacaoAtual,
            QntdProcura = qntdProcura,
            IdCliente = idCliente
        };

        // Act
        var response = await _client.PostAsJsonAsync("/atividades", novaAtividade);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    
    [Fact]
    public async Task AtualizaCliente_RetornaClienteAtualizado_SeClienteExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        int clienteId = 1; 
        var atualizarCliente = new Cliente()
        {
            CpfClie = "12345678909", 
            NmClie = "Carlos Silva",
            SenhaClie = "novaSenha123",
            IdEndereco = 1 
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/clientes/{clienteId}", atualizarCliente);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var clienteRetornado = await response.Content.ReadFromJsonAsync<Cliente>();
        Assert.Equal(atualizarCliente.CpfClie, clienteRetornado.CpfClie);
        Assert.Equal(atualizarCliente.NmClie, clienteRetornado.NmClie);
        Assert.Equal(atualizarCliente.SenhaClie, clienteRetornado.SenhaClie);
        Assert.Equal(atualizarCliente.IdEndereco, clienteRetornado.IdEndereco);
    }


    [Fact]
    public async Task AtualizaAtividade_RetornaAtividadeAtualizada_SeAtividadeExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        int atividadeId = 1; 
        var atualizarAtividade = new Atividades()
        {
            ClimaAtual = 22, 
            DemandaProduto = "Al",
            PrecoVariado = 250.00m,
            LocalizacaoAtual = "Centro da Cidade",
            QntdProcura = 15,
            IdCliente = 1,
            IdProduto = 1
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/atividades/{atividadeId}", atualizarAtividade);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var atividadeRetornada = await response.Content.ReadFromJsonAsync<Atividades>();
        Assert.Equal(atualizarAtividade.ClimaAtual, atividadeRetornada.ClimaAtual);
        Assert.Equal(atualizarAtividade.DemandaProduto, atividadeRetornada.DemandaProduto);
        Assert.Equal(atualizarAtividade.PrecoVariado, atividadeRetornada.PrecoVariado);
        Assert.Equal(atualizarAtividade.LocalizacaoAtual, atividadeRetornada.LocalizacaoAtual);
        Assert.Equal(atualizarAtividade.QntdProcura, atividadeRetornada.QntdProcura);
        Assert.Equal(atualizarAtividade.IdCliente, atividadeRetornada.IdCliente);
    }


    
    [Fact]
    public async Task DeletaAtividade_RetornaNoContent_SeAtividadeExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        int atividadeId = 1;

        // Act
        var response = await _client.DeleteAsync($"/atividades/{atividadeId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeletaAtividade_RetornaStatus404_SeAtividadeNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Arrange
        int atividadeId = 999;

        // Act
        var response = await _client.DeleteAsync($"/atividades/{atividadeId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task DeletaAtividade_RetornaForbidden_SeNaoForAdmin()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Arrange
        int atividadeId = 999;

        // Act
        var response = await _client.DeleteAsync($"/atividades/{atividadeId}");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    


}
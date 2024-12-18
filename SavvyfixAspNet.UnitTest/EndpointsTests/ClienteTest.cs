using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SavvyfixAspNet.Domain.Entities;
using Xunit;

namespace SavvyfixAspNet.UnitTest;

[Collection("Integration Tests")]
public class ClienteTest
{
    private readonly HttpClient _client;
    private readonly IConfiguration? _config;
    private readonly TokenService _tokenService;

    public ClienteTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _config = factory.Services.GetService<IConfiguration>();
        _tokenService = new TokenService(_config);
    }
    
    [Fact]
    public async Task GetClientes_RetornaListaDeClientes()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Act
        var response = await _client.GetAsync("/clientes");

        // Assert
        response.EnsureSuccessStatusCode();
        var clientes = await response.Content.ReadFromJsonAsync<IEnumerable<Cliente>>();
        
        Assert.NotNull(clientes);
        Assert.NotEmpty(clientes);
    }
    
    [Fact]
    public async Task GetClientesSemAutorizacao_RetornaUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/clientes");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task GetClienteById_RetornaCliente_SeClienteExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Arrange
        long clienteId = 1;

        // Act
        var response = await _client.GetAsync($"/clientes/{clienteId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var cliente = await response.Content.ReadFromJsonAsync<Cliente>();

        Assert.NotNull(cliente);
    }

    
    [Fact]
    public async Task GetClienteById_RetornaStatus404_SeClienteNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Arrange
        long clienteId = 999; 

        // Act
        var response = await _client.GetAsync($"/clientes/{clienteId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    
    [Fact]
    public async Task CriaCliente_RetornaClienteCadastrado()
    {
        
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        var novoCliente = new Cliente()
        {
            CpfClie = "14100872046", 
            NmClie = "João Silva",
            SenhaClie = "SenhaSegura123",
            IdEndereco = 1 
        };

        // Act
        var response = await _client.PostAsJsonAsync("/clientes", novoCliente);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var clienteCadastrado = await response.Content.ReadFromJsonAsync<Cliente>();
        Assert.Equal(novoCliente.CpfClie, clienteCadastrado.CpfClie);
        Assert.Equal(novoCliente.NmClie, clienteCadastrado.NmClie);
        Assert.Equal(novoCliente.SenhaClie, clienteCadastrado.SenhaClie);
        Assert.Equal(novoCliente.IdEndereco, clienteCadastrado.IdEndereco);
    }

    
    [Theory]
    [InlineData(null, "João Silva", "SenhaSegura123", 1L)] // CPF nulo
    [InlineData("", "João Silva", "SenhaSegura123", 1L)]  // CPF vazio
    [InlineData("123123", "João Silva", "SenhaSegura123", 1L)]  // CPF inválido
    [InlineData("12345678909", "", "SenhaSegura123", 1L)] // Nome vazio
    [InlineData("12345678909", "João Silva", "", 1L)]     // Senha vazia
    [InlineData("12345678909", "João Silva", "SenhaSegura123", null)] // ID de endereço nulo
    [InlineData("12345678909", "João Silva", "SenhaSegura123", 999L)] // ID de endereço inexistente
    public async Task CriaCliente_RetornaErro_SeVariosAtributosInvalidos(string cpf, string nome, string senha, long? idEndereco)
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        var novoCliente = new Cliente()
        {
            CpfClie = cpf,
            NmClie = nome,
            SenhaClie = senha,
            IdEndereco = idEndereco
        };
    
        // Act
        var response = await _client.PostAsJsonAsync("/clientes", novoCliente);

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
    public async Task AtualizaCliente_RetornaStatus404_SeClienteNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        
        // Arrange
        int clienteId = 999; 
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
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    
    [Fact]
    public async Task DeletaCliente_RetornaNoContent_SeClienteExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Arrange
        int clienteId = 1; 

        // Act
        var response = await _client.DeleteAsync($"/clientes/{clienteId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    
    [Fact]
    public async Task DeletaCliente_RetornaStatus404_SeClienteNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int clienteId = 999; 

        // Act
        var response = await _client.DeleteAsync($"/clientes/{clienteId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task DeletaCliente_RetornaForbidden_SeNaoForAdmin()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int clienteId = 999; 

        // Act
        var response = await _client.DeleteAsync($"/clientes/{clienteId}");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }


}
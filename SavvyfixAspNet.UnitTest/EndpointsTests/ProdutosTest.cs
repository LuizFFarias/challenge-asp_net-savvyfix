using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavvyfixAspNet.Domain.Entities;
using SavvyfixAspNet.UnitTest;
using Xunit;
using static System.Net.HttpStatusCode;

[Collection("Integration Tests")]
public class ProdutosUnitTest
{
    private readonly HttpClient _client;
    private readonly IConfiguration? _config;
    private readonly TokenService _tokenService;

    public ProdutosUnitTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _config = factory.Services.GetService<IConfiguration>();
        _tokenService = new TokenService(_config);
    }

    [Fact]
    public async Task GetProdutos_RetornaListaDeProdutos()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        // Act
        var response = await _client.GetAsync("/produtos");

        // Assert
        response.EnsureSuccessStatusCode();
        var produtos = await response.Content.ReadFromJsonAsync<IEnumerable<Produto>>();
        
        Assert.NotNull(produtos);
        Assert.NotEmpty(produtos);
    }
    
    [Fact]
    public async Task GetProdutosSemAutorizacao_RetornaUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/produtos");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task GetProdutoById_RetornaProduct_SeProdutoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 1;

        // Act
        var response = await _client.GetAsync($"/produtos/{produtoId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var product = await response.Content.ReadFromJsonAsync<Produto>();

        Assert.NotNull(product);
    }
    
    [Fact]
    public async Task GetProdutoById_RetornaStatus404_SeProdutoNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 999; 

        // Act
        var response = await _client.GetAsync($"/produtos/{produtoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task CriaProduto_RetornaProdutoCadastrado()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        var novoProduto = new Produto()
        {
            NmProd = "Tenis Nike",
            DescProd = "Tenis para corrida",
            MarcaProd = "Nike",
            PrecoFixo = 600,
            Img = "Teste"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/produtos", novoProduto);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var produtoCadastrado = await response.Content.ReadFromJsonAsync<Produto>();
        Assert.Equal(novoProduto.NmProd, produtoCadastrado.NmProd);
        Assert.Equal(novoProduto.PrecoFixo, produtoCadastrado.PrecoFixo);
        Assert.Equal(novoProduto.DescProd, produtoCadastrado.DescProd);
        Assert.Equal(novoProduto.MarcaProd, produtoCadastrado.MarcaProd);
        Assert.Equal(novoProduto.Img, produtoCadastrado.Img);
        
    }
    
    [Theory]
    [InlineData(null, "Tenis para corrida", "Nike", 600, "Teste")] // Nome nulo
    [InlineData("", "Tenis para corrida", "Nike", 600, "Teste")]  // Nome vazio
    [InlineData("Tenis Nike", "", "Nike", 600, "Teste")]         // Descrição vazia
    [InlineData("Tenis Nike", "Tenis para corrida", "", 600, "Teste")] // Marca vazia
    [InlineData("Tenis Nike", "Tenis para corrida", "Nike", -1, "Teste")] // Preço negativo
    public async Task CriaProduto_RetornaErro_SeVariosAtributosInvalidos( string nome, string descricao, string marca, decimal preco, string imagem)
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        var novoProduto = new Produto()
        {
            NmProd = nome,
            DescProd = descricao,
            MarcaProd = marca,
            PrecoFixo = preco,
            Img = imagem
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/produtos", novoProduto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); 
        
    }
    
    [Fact]
    public async Task AtualizaProduto_RetornaProdutoAtualizado_SeProdutoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 1;
        var atualizarProduto = new Produto()
        {
            IdProd = produtoId,
            NmProd = "Tenis Nike",
            DescProd = "Tenis Casual",
            MarcaProd = "Nike",
            PrecoFixo = 700,
            Img =  "Teste"
            
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/produtos/{produtoId}", atualizarProduto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var produtoRetornado = await response.Content.ReadFromJsonAsync<Produto>();
        Assert.Equal(atualizarProduto.NmProd, produtoRetornado.NmProd);
        Assert.Equal(atualizarProduto.PrecoFixo, produtoRetornado.PrecoFixo);
        Assert.Equal(atualizarProduto.DescProd, produtoRetornado.DescProd);
        Assert.Equal(atualizarProduto.MarcaProd, produtoRetornado.MarcaProd);
        Assert.Equal(atualizarProduto.Img, produtoRetornado.Img);
    }

    [Fact]
    public async Task AtulizaProduto_RetornaProdutoStatus404_SeProdutoNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 999;
        var atualiazaProduto = new Produto()
        {
            IdProd = produtoId,
            NmProd = "Tenis Nike",
            DescProd = "Tenis Casual",
            MarcaProd = "Nike",
            PrecoFixo = 700,
            Img = "Teste"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/produtos/{produtoId}", atualiazaProduto);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task DeletaProduto_RetornaNoContent_SeProdutoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 2;

        // Act
        var response = await _client.DeleteAsync($"/produtos/{produtoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task DeletaProduto_RetornaStatus404_SeProdutoNaoExiste()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenAdminJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 999;

        // Act
        var response = await _client.DeleteAsync($"/produtos/{produtoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task DeletaProduto_RetornaForbidden_SeNaoForAdmin()
    {
        // Gera o token
        string userToken = _tokenService.GerarTokenUserJwtDeTeste();

        // Adiciona o token ao cabeçalho de autorização
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        // Arrange
        int produtoId = 999;

        // Act
        var response = await _client.DeleteAsync($"/produtos/{produtoId}");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

}
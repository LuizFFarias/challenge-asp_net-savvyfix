using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Data;
using System.Linq;
using SavvyfixAspNet.Domain.Entities;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove a configuração existente do banco de dados
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SavvyfixMetadataDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Configura o banco de dados InMemory para testes
            services.AddDbContext<SavvyfixMetadataDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Popula o banco de dados InMemory
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SavvyfixMetadataDbContext>();
            db.Database.EnsureCreated();
            SeedDatabase(db);
        });
    }

    private void SeedDatabase(SavvyfixMetadataDbContext dbContext)
    {
        if (!dbContext.Produtos.Any())
        {
            dbContext.Produtos.Add(new Produto { 
                IdProd = 1, 
                NmProd = "Tenis Adidas", 
                DescProd = "Tenis para corrida", 
                MarcaProd = "Adidas", 
                PrecoFixo = 450, 
                Img = "teste" });
            dbContext.Produtos.Add(new Produto
            {
                IdProd = 2,
                NmProd = "Tenis Nike",
                DescProd = "Tenis esportivo de alto performance",
                MarcaProd = "Nike",
                PrecoFixo = 750,
                Img = "tenis_nike_teste"
            });
            dbContext.SaveChanges();
        }
    }
}
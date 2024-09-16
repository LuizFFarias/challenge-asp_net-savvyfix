using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SavvyfixAspNet.Api.Configuration.Routes;
using SavvyfixAspNet.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SavvyfixMetadataDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("FiapOracleConnection"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProdutoEndpoints();
app.MapEnderecoEndpoints();
app.MapClienteEndpoints();
app.MapAtividadesEndpoints();
app.MapCompraEndpoint();

app.Run();
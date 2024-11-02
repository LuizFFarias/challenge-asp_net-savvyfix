using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SavvyfixAspNet.Data;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class JwtAuthentication
{
    public static void MapJwtAuthenticationEndpoints(this WebApplication app)
    {
        var authenticationGroup = app.MapGroup("/security");

        authenticationGroup.MapPost("/gerar-token", async (SavvyfixMetadataDbContext dbContext, IConfiguration config, string cpf, string senha) =>
        {
            // Busca o cliente com as roles associadas
            var cliente = await dbContext.Clientes
                .Include(c => c.ClienteRoles) // Inclui a tabela de relacionamento
                .ThenInclude(cr => cr.Roles) // Inclui a entidade Role através de ClienteRole
                .FirstOrDefaultAsync(u => u.CpfClie == cpf);

            if (cliente == null || cliente.SenhaClie != senha) // Aqui idealmente a senha deve ser verificada com hash
            {
                return Results.Unauthorized();
            }

            // Configura a chave e credenciais para gerar o token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cria os claims do token, incluindo os papéis do cliente
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, cliente.CpfClie),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Adiciona um claim para cada role associada ao cliente
            claims.AddRange(cliente.ClienteRoles.Select(cr => new Claim(ClaimTypes.Role, cr.Roles.NomeRole)));

            // Gera o token JWT
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(new { token = tokenString });
        });
    }



}
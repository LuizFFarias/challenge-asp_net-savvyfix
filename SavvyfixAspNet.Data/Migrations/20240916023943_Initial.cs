using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyfixAspNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    IdEndereco = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CepEndereco = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    RuaEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NumEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BairroEndeereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CidadeEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EstadoEndereco = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    PaisEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProd = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PrecoFixo = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    MarcaProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DescProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NmProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Img = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProd);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    CpfClie = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    NmClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SenhaClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdEndereco = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Enderecos_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    IdAtividades = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClimaAtual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DemandaProduto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HorarioAtual = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LocalizacaoAtual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PrecoVariado = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    QntdProcura = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.IdAtividades);
                    table.ForeignKey(
                        name: "FK_Atividades_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCompra = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    QntdProd = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ValorCompra = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    IdProd = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    IdAtividades = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    NmProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EspcificacoesProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK_Compra_Atividades_IdAtividades",
                        column: x => x.IdAtividades,
                        principalTable: "Atividades",
                        principalColumn: "IdAtividades");
                    table.ForeignKey(
                        name: "FK_Compra_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compra_Produtos_IdProd",
                        column: x => x.IdProd,
                        principalTable: "Produtos",
                        principalColumn: "IdProd",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_IdCliente",
                table: "Atividades",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdAtividades",
                table: "Compra",
                column: "IdAtividades");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdCliente",
                table: "Compra",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdProd",
                table: "Compra",
                column: "IdProd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}

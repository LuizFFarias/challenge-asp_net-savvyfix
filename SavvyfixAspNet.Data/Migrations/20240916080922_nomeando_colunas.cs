using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyfixAspNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class nomeando_colunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Clientes_IdCliente",
                table: "Atividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Enderecos_IdCliente",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Atividades_IdAtividades",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Clientes_IdCliente",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Produtos_IdProd",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "PrecoFixo",
                table: "Produtos",
                newName: "preco_fixo");

            migrationBuilder.RenameColumn(
                name: "NmProd",
                table: "Produtos",
                newName: "nm_prod");

            migrationBuilder.RenameColumn(
                name: "MarcaProd",
                table: "Produtos",
                newName: "marca_prod");

            migrationBuilder.RenameColumn(
                name: "Img",
                table: "Produtos",
                newName: "img_prod");

            migrationBuilder.RenameColumn(
                name: "DescProd",
                table: "Produtos",
                newName: "desc_proc");

            migrationBuilder.RenameColumn(
                name: "IdProd",
                table: "Produtos",
                newName: "id_prod");

            migrationBuilder.RenameColumn(
                name: "RuaEndereco",
                table: "Enderecos",
                newName: "rua_endereco");

            migrationBuilder.RenameColumn(
                name: "PaisEndereco",
                table: "Enderecos",
                newName: "pais");

            migrationBuilder.RenameColumn(
                name: "NumEndereco",
                table: "Enderecos",
                newName: "num_endereco");

            migrationBuilder.RenameColumn(
                name: "EstadoEndereco",
                table: "Enderecos",
                newName: "estado_endereco");

            migrationBuilder.RenameColumn(
                name: "CidadeEndereco",
                table: "Enderecos",
                newName: "cidade_endereco");

            migrationBuilder.RenameColumn(
                name: "CepEndereco",
                table: "Enderecos",
                newName: "cep_endereco");

            migrationBuilder.RenameColumn(
                name: "BairroEndeereco",
                table: "Enderecos",
                newName: "bairro_endereco");

            migrationBuilder.RenameColumn(
                name: "IdEndereco",
                table: "Enderecos",
                newName: "id_endereco");

            migrationBuilder.RenameColumn(
                name: "ValorCompra",
                table: "Compra",
                newName: "valor_compra");

            migrationBuilder.RenameColumn(
                name: "QntdProd",
                table: "Compra",
                newName: "qntd_prod");

            migrationBuilder.RenameColumn(
                name: "NmProd",
                table: "Compra",
                newName: "nm_prod");

            migrationBuilder.RenameColumn(
                name: "IdProd",
                table: "Compra",
                newName: "id_produto");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Compra",
                newName: "id_cliente");

            migrationBuilder.RenameColumn(
                name: "IdAtividades",
                table: "Compra",
                newName: "id_atividades");

            migrationBuilder.RenameColumn(
                name: "EspcificacoesProd",
                table: "Compra",
                newName: "especificacao_prod");

            migrationBuilder.RenameColumn(
                name: "IdCompra",
                table: "Compra",
                newName: "id_compra");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdProd",
                table: "Compra",
                newName: "IX_Compra_id_produto");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdCliente",
                table: "Compra",
                newName: "IX_Compra_id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdAtividades",
                table: "Compra",
                newName: "IX_Compra_id_atividades");

            migrationBuilder.RenameColumn(
                name: "SenhaClie",
                table: "Clientes",
                newName: "senha_clie");

            migrationBuilder.RenameColumn(
                name: "NmClie",
                table: "Clientes",
                newName: "nm_clie");

            migrationBuilder.RenameColumn(
                name: "IdEndereco",
                table: "Clientes",
                newName: "id_endereco");

            migrationBuilder.RenameColumn(
                name: "CpfClie",
                table: "Clientes",
                newName: "cpf_clie");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Clientes",
                newName: "id_cliente");

            migrationBuilder.RenameColumn(
                name: "QntdProcura",
                table: "Atividades",
                newName: "qntd_procura");

            migrationBuilder.RenameColumn(
                name: "PrecoVariado",
                table: "Atividades",
                newName: "preco_variado");

            migrationBuilder.RenameColumn(
                name: "LocalizacaoAtual",
                table: "Atividades",
                newName: "localizacao_atual");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Atividades",
                newName: "id_cliente");

            migrationBuilder.RenameColumn(
                name: "HorarioAtual",
                table: "Atividades",
                newName: "horario_atual");

            migrationBuilder.RenameColumn(
                name: "DemandaProduto",
                table: "Atividades",
                newName: "demanda_produto");

            migrationBuilder.RenameColumn(
                name: "ClimaAtual",
                table: "Atividades",
                newName: "clima_atual");

            migrationBuilder.RenameColumn(
                name: "IdAtividades",
                table: "Atividades",
                newName: "id_atividades");

            migrationBuilder.RenameIndex(
                name: "IX_Atividades_IdCliente",
                table: "Atividades",
                newName: "IX_Atividades_id_cliente");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_fixo",
                table: "Produtos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_compra",
                table: "Compra",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_variado",
                table: "Atividades",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Clientes_id_cliente",
                table: "Atividades",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Enderecos_id_cliente",
                table: "Clientes",
                column: "id_cliente",
                principalTable: "Enderecos",
                principalColumn: "id_endereco",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Atividades_id_atividades",
                table: "Compra",
                column: "id_atividades",
                principalTable: "Atividades",
                principalColumn: "id_atividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Clientes_id_cliente",
                table: "Compra",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Produtos_id_produto",
                table: "Compra",
                column: "id_produto",
                principalTable: "Produtos",
                principalColumn: "id_prod",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Clientes_id_cliente",
                table: "Atividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Enderecos_id_cliente",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Atividades_id_atividades",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Clientes_id_cliente",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Produtos_id_produto",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "preco_fixo",
                table: "Produtos",
                newName: "PrecoFixo");

            migrationBuilder.RenameColumn(
                name: "nm_prod",
                table: "Produtos",
                newName: "NmProd");

            migrationBuilder.RenameColumn(
                name: "marca_prod",
                table: "Produtos",
                newName: "MarcaProd");

            migrationBuilder.RenameColumn(
                name: "img_prod",
                table: "Produtos",
                newName: "Img");

            migrationBuilder.RenameColumn(
                name: "desc_proc",
                table: "Produtos",
                newName: "DescProd");

            migrationBuilder.RenameColumn(
                name: "id_prod",
                table: "Produtos",
                newName: "IdProd");

            migrationBuilder.RenameColumn(
                name: "rua_endereco",
                table: "Enderecos",
                newName: "RuaEndereco");

            migrationBuilder.RenameColumn(
                name: "pais",
                table: "Enderecos",
                newName: "PaisEndereco");

            migrationBuilder.RenameColumn(
                name: "num_endereco",
                table: "Enderecos",
                newName: "NumEndereco");

            migrationBuilder.RenameColumn(
                name: "estado_endereco",
                table: "Enderecos",
                newName: "EstadoEndereco");

            migrationBuilder.RenameColumn(
                name: "cidade_endereco",
                table: "Enderecos",
                newName: "CidadeEndereco");

            migrationBuilder.RenameColumn(
                name: "cep_endereco",
                table: "Enderecos",
                newName: "CepEndereco");

            migrationBuilder.RenameColumn(
                name: "bairro_endereco",
                table: "Enderecos",
                newName: "BairroEndeereco");

            migrationBuilder.RenameColumn(
                name: "id_endereco",
                table: "Enderecos",
                newName: "IdEndereco");

            migrationBuilder.RenameColumn(
                name: "valor_compra",
                table: "Compra",
                newName: "ValorCompra");

            migrationBuilder.RenameColumn(
                name: "qntd_prod",
                table: "Compra",
                newName: "QntdProd");

            migrationBuilder.RenameColumn(
                name: "nm_prod",
                table: "Compra",
                newName: "NmProd");

            migrationBuilder.RenameColumn(
                name: "id_produto",
                table: "Compra",
                newName: "IdProd");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "Compra",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "id_atividades",
                table: "Compra",
                newName: "IdAtividades");

            migrationBuilder.RenameColumn(
                name: "especificacao_prod",
                table: "Compra",
                newName: "EspcificacoesProd");

            migrationBuilder.RenameColumn(
                name: "id_compra",
                table: "Compra",
                newName: "IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_id_produto",
                table: "Compra",
                newName: "IX_Compra_IdProd");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_id_cliente",
                table: "Compra",
                newName: "IX_Compra_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_id_atividades",
                table: "Compra",
                newName: "IX_Compra_IdAtividades");

            migrationBuilder.RenameColumn(
                name: "senha_clie",
                table: "Clientes",
                newName: "SenhaClie");

            migrationBuilder.RenameColumn(
                name: "nm_clie",
                table: "Clientes",
                newName: "NmClie");

            migrationBuilder.RenameColumn(
                name: "id_endereco",
                table: "Clientes",
                newName: "IdEndereco");

            migrationBuilder.RenameColumn(
                name: "cpf_clie",
                table: "Clientes",
                newName: "CpfClie");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "Clientes",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "qntd_procura",
                table: "Atividades",
                newName: "QntdProcura");

            migrationBuilder.RenameColumn(
                name: "preco_variado",
                table: "Atividades",
                newName: "PrecoVariado");

            migrationBuilder.RenameColumn(
                name: "localizacao_atual",
                table: "Atividades",
                newName: "LocalizacaoAtual");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "Atividades",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "horario_atual",
                table: "Atividades",
                newName: "HorarioAtual");

            migrationBuilder.RenameColumn(
                name: "demanda_produto",
                table: "Atividades",
                newName: "DemandaProduto");

            migrationBuilder.RenameColumn(
                name: "clima_atual",
                table: "Atividades",
                newName: "ClimaAtual");

            migrationBuilder.RenameColumn(
                name: "id_atividades",
                table: "Atividades",
                newName: "IdAtividades");

            migrationBuilder.RenameIndex(
                name: "IX_Atividades_id_cliente",
                table: "Atividades",
                newName: "IX_Atividades_IdCliente");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoFixo",
                table: "Produtos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorCompra",
                table: "Compra",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoVariado",
                table: "Atividades",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Clientes_IdCliente",
                table: "Atividades",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Enderecos_IdCliente",
                table: "Clientes",
                column: "IdCliente",
                principalTable: "Enderecos",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Atividades_IdAtividades",
                table: "Compra",
                column: "IdAtividades",
                principalTable: "Atividades",
                principalColumn: "IdAtividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Clientes_IdCliente",
                table: "Compra",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Produtos_IdProd",
                table: "Compra",
                column: "IdProd",
                principalTable: "Produtos",
                principalColumn: "IdProd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

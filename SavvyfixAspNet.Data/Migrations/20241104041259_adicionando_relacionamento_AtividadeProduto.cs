using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyfixAspNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class adicionando_relacionamento_AtividadeProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "preco_fixo",
                table: "Produtos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_compra",
                table: "Compras",
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

            migrationBuilder.AddColumn<long>(
                name: "id_produto",
                table: "Atividades",
                type: "NUMBER(19)",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_id_produto",
                table: "Atividades",
                column: "id_produto");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Produtos_id_produto",
                table: "Atividades",
                column: "id_produto",
                principalTable: "Produtos",
                principalColumn: "id_prod",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Produtos_id_produto",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_id_produto",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "id_produto",
                table: "Atividades");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_fixo",
                table: "Produtos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_compra",
                table: "Compras",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco_variado",
                table: "Atividades",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace JwtAuth.Data.Migrations
{
    public partial class CorrecaoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identification",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Identificacao",
                table: "Produtos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identificacao",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Identification",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

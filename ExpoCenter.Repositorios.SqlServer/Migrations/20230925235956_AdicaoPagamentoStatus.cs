using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpoCenter.Repositorios.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoPagamentoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pagamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pagamento");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DesafioServiceNetAPI.Migrations
{
    public partial class DisableAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CepID",
                table: "tbl_ceps",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CepID",
                table: "tbl_ceps",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}

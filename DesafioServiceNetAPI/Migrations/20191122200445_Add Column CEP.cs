using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DesafioServiceNetAPI.Migrations
{
    public partial class AddColumnCEP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "tbl_clients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "tbl_clients");

            migrationBuilder.DropColumn(
                name: "State",
                table: "tbl_clients");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "tbl_clients",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CepId",
                table: "tbl_clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tbl_ceps",
                columns: table => new
                {
                    CepID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ceps", x => x.CepID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_clients_CepId",
                table: "tbl_clients",
                column: "CepId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_clients_tbl_ceps_CepId",
                table: "tbl_clients",
                column: "CepId",
                principalTable: "tbl_ceps",
                principalColumn: "CepID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_clients_tbl_ceps_CepId",
                table: "tbl_clients");

            migrationBuilder.DropTable(
                name: "tbl_ceps");

            migrationBuilder.DropIndex(
                name: "IX_tbl_clients_CepId",
                table: "tbl_clients");

            migrationBuilder.DropColumn(
                name: "CepId",
                table: "tbl_clients");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "tbl_clients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "tbl_clients",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tbl_clients",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "tbl_clients",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}

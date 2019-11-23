using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DesafioServiceNetAPI.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ceps",
                columns: table => new
                {
                    CepID = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ceps", x => x.CepID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 70, nullable: false),
                    NumberAddress = table.Column<string>(maxLength: 10, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false),
                    CepId = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_clients", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_tbl_clients_tbl_ceps_CepId",
                        column: x => x.CepId,
                        principalTable: "tbl_ceps",
                        principalColumn: "CepID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_clients_tbl_users_UserID",
                        column: x => x.UserID,
                        principalTable: "tbl_users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_clients_CepId",
                table: "tbl_clients",
                column: "CepId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_clients_Name",
                table: "tbl_clients",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_clients_UserID",
                table: "tbl_clients",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_users_Email",
                table: "tbl_users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_clients");

            migrationBuilder.DropTable(
                name: "tbl_ceps");

            migrationBuilder.DropTable(
                name: "tbl_users");
        }
    }
}

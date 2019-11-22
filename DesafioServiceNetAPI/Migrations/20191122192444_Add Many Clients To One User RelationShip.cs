using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioServiceNetAPI.Migrations
{
    public partial class AddManyClientsToOneUserRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "tbl_clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_clients_UserID",
                table: "tbl_clients",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_clients_tbl_users_UserID",
                table: "tbl_clients",
                column: "UserID",
                principalTable: "tbl_users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_clients_tbl_users_UserID",
                table: "tbl_clients");

            migrationBuilder.DropIndex(
                name: "IX_tbl_clients_UserID",
                table: "tbl_clients");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "tbl_clients");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ex9_ORM_EFCore_Console.Migrations
{
    public partial class AddRelationBetweenClientAndClientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ClientInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfo_ClientId",
                table: "ClientInfo",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInfo_Clients_ClientId",
                table: "ClientInfo",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInfo_Clients_ClientId",
                table: "ClientInfo");

            migrationBuilder.DropIndex(
                name: "IX_ClientInfo_ClientId",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientInfo");
        }
    }
}

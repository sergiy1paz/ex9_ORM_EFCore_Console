using Microsoft.EntityFrameworkCore.Migrations;

namespace ex9_ORM_EFCore_Console.Migrations
{
    public partial class AddMoreRelationForClientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BankClient",
                columns: table => new
                {
                    BanksId = table.Column<int>(type: "int", nullable: false),
                    ClientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankClient", x => new { x.BanksId, x.ClientsId });
                    table.ForeignKey(
                        name: "FK_BankClient_Banks_BanksId",
                        column: x => x.BanksId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankClient_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BankClient_ClientsId",
                table: "BankClient",
                column: "ClientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_ClientId",
                table: "Accounts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_ClientId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "BankClient");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Accounts");
        }
    }
}

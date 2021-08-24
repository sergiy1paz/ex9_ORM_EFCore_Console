using Microsoft.EntityFrameworkCore.Migrations;

namespace ex9_ORM_EFCore_Console.Migrations
{
    public partial class AddAlternateKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "card_number",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Clients_UserName",
                table: "Clients",
                column: "UserName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Banks_Name",
                table: "Banks",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_card_number",
                table: "Accounts",
                column: "card_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Clients_UserName",
                table: "Clients");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Banks_Name",
                table: "Banks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_card_number",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "card_number",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

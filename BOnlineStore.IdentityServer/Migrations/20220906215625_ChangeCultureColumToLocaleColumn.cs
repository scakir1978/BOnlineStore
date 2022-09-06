using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BOnlineStore.IdentityServer.Migrations
{
    public partial class ChangeCultureColumToLocaleColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Culture",
                table: "AspNetUsers",
                newName: "Locale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Locale",
                table: "AspNetUsers",
                newName: "Culture");
        }
    }
}

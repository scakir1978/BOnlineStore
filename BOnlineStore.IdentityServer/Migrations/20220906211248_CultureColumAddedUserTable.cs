using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BOnlineStore.IdentityServer.Migrations
{
    public partial class CultureColumAddedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Culture",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Culture",
                table: "AspNetUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BOnlineStore.IdentityServer.Migrations
{
    public partial class Tenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_Adress1 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_Adress2 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_CountryName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_StateOrCityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_CityOrCountyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_DistrictName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Adress_PostalCode = table.Column<int>(type: "int", nullable: true),
                    TaxInformation_TaxAdministration = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TaxInformation_TaxNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers");
        }
    }
}

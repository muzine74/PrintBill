using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCompagnyPricing1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCompagnyPricings",
                columns: table => new
                {
                    EmployeeCompagnyPricingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyPricingCalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmplyeePaiment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCompagnyPricings", x => x.EmployeeCompagnyPricingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCompagnyPricings");
        }
    }
}

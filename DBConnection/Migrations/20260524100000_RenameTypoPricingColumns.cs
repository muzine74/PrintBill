using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class RenameTypoPricingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name:  "CopagnyBenifictPrice",
                table: "CompanyPricingCalendars",
                newName: "CompanyBenefitPrice");

            migrationBuilder.RenameColumn(
                name:  "Emplyeepaiment",
                table: "CompanyPricingCalendars",
                newName: "EmployeePayment");

            migrationBuilder.RenameColumn(
                name:  "EmplyeePaiment",
                table: "EmployeeCompagnyPricings",
                newName: "EmployeePayment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name:  "CompanyBenefitPrice",
                table: "CompanyPricingCalendars",
                newName: "CopagnyBenifictPrice");

            migrationBuilder.RenameColumn(
                name:  "EmployeePayment",
                table: "CompanyPricingCalendars",
                newName: "Emplyeepaiment");

            migrationBuilder.RenameColumn(
                name:  "EmployeePayment",
                table: "EmployeeCompagnyPricings",
                newName: "EmplyeePaiment");
        }
    }
}

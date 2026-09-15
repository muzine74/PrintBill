using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    // Écrite à la main (comme 20260524100000_RenameTypoPricingColumns) : dotnet-ef v10 est
    // incompatible avec le package EF Core 8 référencé par TimeGuardAPI (TypeLoadException sur
    // CSharpHelper.Identifier). Pas de Designer.cs / snapshot associé — appliquer via le script
    // SQL manuel fourni avec la fonctionnalité "Paiements employés".
    public partial class AddEmployeePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayments",
                columns: table => new
                {
                    EmployeePaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId          = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId        = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodStart       = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd         = table.Column<DateOnly>(type: "date", nullable: false),
                    AmountPaid        = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note              = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UpdatedAt         = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayments", x => x.EmployeePaymentId);
                    table.ForeignKey(
                        name: "FK_EmployeePayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_Tenant_Employee_Period",
                table: "EmployeePayments",
                columns: new[] { "TenantId", "EmployeeId", "PeriodStart", "PeriodEnd" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "EmployeePayments");
        }
    }
}

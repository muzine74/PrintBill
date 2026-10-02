using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>EmployeeCompanies.PayMode : mode de rémunération d'un employé chez UNE compagnie
    /// ("Hourly" / "Visit"), NULL = mode par défaut de l'employé (Employees.PayMode).</summary>
    public partial class AddEmployeeCompanyPayMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeeCompanies', N'PayMode') IS NULL
                    ALTER TABLE [EmployeeCompanies] ADD [PayMode] NVARCHAR(20) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeeCompanies', N'PayMode') IS NOT NULL
                    ALTER TABLE [EmployeeCompanies] DROP COLUMN [PayMode];
            ");
        }
    }
}

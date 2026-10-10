using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Employees.TpsNumber / TvqNumber : numéros de taxes de l'employé (NULL = pas de taxe sur ses paiements).
    /// Ajout seul, aucune donnée modifiée.</summary>
    public partial class AddEmployeeTaxNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Employees', N'TpsNumber') IS NULL
    ALTER TABLE [Employees] ADD [TpsNumber] NVARCHAR(50) NULL;
IF COL_LENGTH(N'Employees', N'TvqNumber') IS NULL
    ALTER TABLE [Employees] ADD [TvqNumber] NVARCHAR(50) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Employees', N'TpsNumber') IS NOT NULL ALTER TABLE [Employees] DROP COLUMN [TpsNumber];
IF COL_LENGTH(N'Employees', N'TvqNumber') IS NOT NULL ALTER TABLE [Employees] DROP COLUMN [TvqNumber];
            ");
        }
    }
}

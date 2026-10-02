using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Hiérarchie d'équipe : Employees.IsTeamLead (chef d'équipe) + Employees.ManagerId (chef / responsable
    /// direct). Additif : tous les employés existants restent non-chefs et sans chef (à compléter dans l'application).</summary>
    public partial class AddTeamHierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Employees', N'IsTeamLead') IS NULL
                    ALTER TABLE [Employees] ADD [IsTeamLead] BIT NOT NULL CONSTRAINT [DF_Employees_IsTeamLead] DEFAULT 0;
                IF COL_LENGTH(N'Employees', N'ManagerId') IS NULL
                    ALTER TABLE [Employees] ADD [ManagerId] UNIQUEIDENTIFIER NULL;
            ");
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Employees_ManagerId')
                    CREATE INDEX [IX_Employees_ManagerId] ON [Employees]([ManagerId]);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Employees_ManagerId')
                    DROP INDEX [IX_Employees_ManagerId] ON [Employees];
                IF COL_LENGTH(N'Employees', N'ManagerId') IS NOT NULL
                    ALTER TABLE [Employees] DROP COLUMN [ManagerId];
                IF COL_LENGTH(N'Employees', N'IsTeamLead') IS NOT NULL
                BEGIN
                    ALTER TABLE [Employees] DROP CONSTRAINT [DF_Employees_IsTeamLead];
                    ALTER TABLE [Employees] DROP COLUMN [IsTeamLead];
                END
            ");
        }
    }
}

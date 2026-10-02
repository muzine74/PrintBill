using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Plusieurs plages horaires par jour : un seul pointage par compagnie et par jour (les visites restent
    /// comptées une fois), avec le total des heures (WorkedHours) et le détail des plages (TimeRanges).</summary>
    public partial class AddTimeLogRanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeeTimeLogs', N'WorkedHours') IS NULL
                    ALTER TABLE [EmployeeTimeLogs] ADD [WorkedHours] DECIMAL(9,2) NULL;
                IF COL_LENGTH(N'EmployeeTimeLogs', N'TimeRanges') IS NULL
                    ALTER TABLE [EmployeeTimeLogs] ADD [TimeRanges] NVARCHAR(400) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeeTimeLogs', N'TimeRanges') IS NOT NULL
                    ALTER TABLE [EmployeeTimeLogs] DROP COLUMN [TimeRanges];
                IF COL_LENGTH(N'EmployeeTimeLogs', N'WorkedHours') IS NOT NULL
                    ALTER TABLE [EmployeeTimeLogs] DROP COLUMN [WorkedHours];
            ");
        }
    }
}

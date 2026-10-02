using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>
    /// Mode de rémunération employé, indépendant du mode de facturation compagnie (4 combinaisons).
    ///   Employees.PayMode (NULL = par visite, "Hourly" = par heure)
    ///   EmployeeTimeLogs.PayHourlyRate (taux payé figé quand l'employé est payé à l'heure)
    /// Reprise : le type « Contractuel horaire » (staging uniquement, jamais en prod) devient PayMode = Hourly.
    /// </summary>
    public partial class AddEmployeePayMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Employees', N'PayMode') IS NULL
                    ALTER TABLE [Employees] ADD [PayMode] NVARCHAR(20) NULL;
                IF COL_LENGTH(N'EmployeeTimeLogs', N'PayHourlyRate') IS NULL
                    ALTER TABLE [EmployeeTimeLogs] ADD [PayHourlyRate] DECIMAL(18,2) NULL;
            ");
            migrationBuilder.Sql(@"
                UPDATE [Employees] SET [PayMode] = N'Hourly', [EmployeeType] = N'À la tâche'
                WHERE [EmployeeType] = N'Contractuel horaire';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeeTimeLogs', N'PayHourlyRate') IS NOT NULL
                    ALTER TABLE [EmployeeTimeLogs] DROP COLUMN [PayHourlyRate];
                IF COL_LENGTH(N'Employees', N'PayMode') IS NOT NULL
                    ALTER TABLE [Employees] DROP COLUMN [PayMode];
            ");
        }
    }
}

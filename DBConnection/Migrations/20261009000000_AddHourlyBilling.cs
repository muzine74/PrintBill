using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>
    /// Facturation par heure (compagnie « par heure » + employé « Contractuel horaire ») — colonnes NULLABLES
    /// uniquement : les compagnies, affectations et pointages existants restent « par tâche », inchangés.
    ///   Companies.BillingMode (NULL = par tâche), HourlyClientRate, HourlyEmployeeRate
    ///   EmployeeCompanies.HourlyRate (taux payé spécifique)
    ///   EmployeeTimeLogs.ClientHourlyRate (taux client figé au pointage)
    /// </summary>
    public partial class AddHourlyBilling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Companies', N'BillingMode') IS NULL
                    ALTER TABLE [Companies] ADD [BillingMode] NVARCHAR(20) NULL;
                IF COL_LENGTH(N'Companies', N'HourlyClientRate') IS NULL
                    ALTER TABLE [Companies] ADD [HourlyClientRate] DECIMAL(18,2) NULL;
                IF COL_LENGTH(N'Companies', N'HourlyEmployeeRate') IS NULL
                    ALTER TABLE [Companies] ADD [HourlyEmployeeRate] DECIMAL(18,2) NULL;
                IF COL_LENGTH(N'EmployeeCompanies', N'HourlyRate') IS NULL
                    ALTER TABLE [EmployeeCompanies] ADD [HourlyRate] DECIMAL(18,2) NULL;
                IF COL_LENGTH(N'EmployeeTimeLogs', N'ClientHourlyRate') IS NULL
                    ALTER TABLE [EmployeeTimeLogs] ADD [ClientHourlyRate] DECIMAL(18,2) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeeTimeLogs', N'ClientHourlyRate') IS NOT NULL
                    ALTER TABLE [EmployeeTimeLogs] DROP COLUMN [ClientHourlyRate];
                IF COL_LENGTH(N'EmployeeCompanies', N'HourlyRate') IS NOT NULL
                    ALTER TABLE [EmployeeCompanies] DROP COLUMN [HourlyRate];
                IF COL_LENGTH(N'Companies', N'HourlyEmployeeRate') IS NOT NULL
                    ALTER TABLE [Companies] DROP COLUMN [HourlyEmployeeRate];
                IF COL_LENGTH(N'Companies', N'HourlyClientRate') IS NOT NULL
                    ALTER TABLE [Companies] DROP COLUMN [HourlyClientRate];
                IF COL_LENGTH(N'Companies', N'BillingMode') IS NOT NULL
                    ALTER TABLE [Companies] DROP COLUMN [BillingMode];
            ");
        }
    }
}

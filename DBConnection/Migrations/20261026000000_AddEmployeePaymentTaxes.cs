using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>EmployeePayments / EmployeePaymentHistories : TpsAmount, TvqAmount (taxes figées à l'enregistrement).
    /// NULL pour les lignes existantes. Ajout seul, aucune donnée modifiée.</summary>
    public partial class AddEmployeePaymentTaxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'EmployeePayments', N'TpsAmount') IS NULL
    ALTER TABLE [EmployeePayments] ADD [TpsAmount] DECIMAL(18,2) NULL, [TvqAmount] DECIMAL(18,2) NULL;
IF COL_LENGTH(N'EmployeePaymentHistories', N'TpsAmount') IS NULL
    ALTER TABLE [EmployeePaymentHistories] ADD [TpsAmount] DECIMAL(18,2) NULL, [TvqAmount] DECIMAL(18,2) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'EmployeePayments', N'TpsAmount') IS NOT NULL
    ALTER TABLE [EmployeePayments] DROP COLUMN [TpsAmount], [TvqAmount];
IF COL_LENGTH(N'EmployeePaymentHistories', N'TpsAmount') IS NOT NULL
    ALTER TABLE [EmployeePaymentHistories] DROP COLUMN [TpsAmount], [TvqAmount];
            ");
        }
    }
}

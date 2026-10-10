using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>EmployeePayments.PaidDates : journées payées cochées (yyyy-MM-dd, séparées par des virgules).
    /// NULL pour les paiements existants (= non mémorisé). Ajout seul, aucune donnée modifiée.</summary>
    public partial class AddEmployeePaymentPaidDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeePayments', N'PaidDates') IS NULL
                    ALTER TABLE [EmployeePayments] ADD [PaidDates] NVARCHAR(4000) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'EmployeePayments', N'PaidDates') IS NOT NULL
                    ALTER TABLE [EmployeePayments] DROP COLUMN [PaidDates];
            ");
        }
    }
}

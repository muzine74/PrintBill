using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>EmployeePayments : PaymentDate (date de paiement choisie), IsTransferred (+ TransferredAt).
    /// Les paiements existants sont marqués transférés à la date de leur enregistrement.
    /// EmployeePaymentHistories : PaymentDate, IsTransferred (NULL pour l'historique existant).</summary>
    public partial class AddEmployeePaymentTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'EmployeePayments', N'IsTransferred') IS NULL
BEGIN
    ALTER TABLE [EmployeePayments] ADD
        [PaymentDate]   DATE      NULL,
        [IsTransferred] BIT       NOT NULL CONSTRAINT [DF_EmployeePayments_IsTransferred] DEFAULT 0,
        [TransferredAt] DATETIME2 NULL;
    -- Paiements déjà enregistrés : ils étaient jusqu'ici considérés comme payés à la date de leur
    -- enregistrement → marqués « transférés » à cette date (heure de l'Est ≈ UTC − 4 h).
    EXEC(N'UPDATE [EmployeePayments]
           SET [IsTransferred] = 1,
               [PaymentDate]   = CAST(DATEADD(HOUR, -4, [UpdatedAt]) AS DATE),
               [TransferredAt] = [UpdatedAt]');
END
IF COL_LENGTH(N'EmployeePaymentHistories', N'IsTransferred') IS NULL
    ALTER TABLE [EmployeePaymentHistories] ADD [PaymentDate] DATE NULL, [IsTransferred] BIT NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'EmployeePayments', N'IsTransferred') IS NOT NULL
BEGIN
    ALTER TABLE [EmployeePayments] DROP CONSTRAINT [DF_EmployeePayments_IsTransferred];
    ALTER TABLE [EmployeePayments] DROP COLUMN [PaymentDate], [IsTransferred], [TransferredAt];
END
IF COL_LENGTH(N'EmployeePaymentHistories', N'IsTransferred') IS NOT NULL
    ALTER TABLE [EmployeePaymentHistories] DROP COLUMN [PaymentDate], [IsTransferred];
            ");
        }
    }
}

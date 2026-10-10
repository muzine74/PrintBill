using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>BankTransactions.IsValidated (+ ValidatedAt / ValidatedBy) : transaction vérifiée par un utilisateur.
    /// Les lignes existantes restent « non validées ». Ajout seul.</summary>
    public partial class AddBankTransactionValidated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'BankTransactions', N'IsValidated') IS NULL
    ALTER TABLE [BankTransactions] ADD [IsValidated] BIT NOT NULL CONSTRAINT [DF_BankTransactions_IsValidated] DEFAULT 0;
IF COL_LENGTH(N'BankTransactions', N'ValidatedAt') IS NULL
    ALTER TABLE [BankTransactions] ADD [ValidatedAt] DATETIME2 NULL;
IF COL_LENGTH(N'BankTransactions', N'ValidatedBy') IS NULL
    ALTER TABLE [BankTransactions] ADD [ValidatedBy] NVARCHAR(256) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'BankTransactions', N'IsValidated') IS NOT NULL
BEGIN
    ALTER TABLE [BankTransactions] DROP CONSTRAINT [DF_BankTransactions_IsValidated];
    ALTER TABLE [BankTransactions] DROP COLUMN [IsValidated];
END
IF COL_LENGTH(N'BankTransactions', N'ValidatedAt') IS NOT NULL ALTER TABLE [BankTransactions] DROP COLUMN [ValidatedAt];
IF COL_LENGTH(N'BankTransactions', N'ValidatedBy') IS NOT NULL ALTER TABLE [BankTransactions] DROP COLUMN [ValidatedBy];
            ");
        }
    }
}

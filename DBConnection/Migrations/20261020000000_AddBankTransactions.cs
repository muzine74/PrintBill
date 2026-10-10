using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Table BankTransactions : lignes de relevé bancaire importées d'un CSV
    /// (date, description, retrait, dépôt). Création seule.</summary>
    public partial class AddBankTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[BankTransactions]', N'U') IS NULL
BEGIN
    CREATE TABLE [BankTransactions] (
        [BankTransactionId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_BankTransactions] PRIMARY KEY,
        [TenantId]          UNIQUEIDENTIFIER NOT NULL,
        [TransactionDate]   DATE             NOT NULL,
        [Description]       NVARCHAR(500)    NOT NULL,
        [Withdrawal]        DECIMAL(18,2)    NULL,
        [Deposit]           DECIMAL(18,2)    NULL,
        [AccountNumber]     NVARCHAR(50)     NULL,
        [LineNumber]        INT              NULL,
        [ImportedAt]        DATETIME2        NOT NULL,
        [ImportedBy]        NVARCHAR(256)    NULL,
        [SourceFile]        NVARCHAR(260)    NULL
    );
    CREATE INDEX [IX_BankTransactions_TenantId_TransactionDate] ON [BankTransactions] ([TenantId], [TransactionDate]);
END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'[BankTransactions]', N'U') IS NOT NULL DROP TABLE [BankTransactions];");
        }
    }
}

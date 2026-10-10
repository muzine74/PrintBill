using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Table BankLexiconEntries : mots-clés du relevé bancaire reliés à une compagnie,
    /// un employé ou une charge. Création seule.</summary>
    public partial class AddBankLexicon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[BankLexiconEntries]', N'U') IS NULL
BEGIN
    CREATE TABLE [BankLexiconEntries] (
        [BankLexiconEntryId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_BankLexiconEntries] PRIMARY KEY,
        [TenantId]   UNIQUEIDENTIFIER NOT NULL,
        [Keyword]    NVARCHAR(100)    NOT NULL,
        [TargetType] NVARCHAR(20)     NOT NULL,
        [TargetId]   UNIQUEIDENTIFIER NOT NULL,
        [CreatedAt]  DATETIME2        NOT NULL,
        [CreatedBy]  NVARCHAR(256)    NULL
    );
    CREATE INDEX [IX_BankLexiconEntries_TenantId] ON [BankLexiconEntries] ([TenantId]);
END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'[BankLexiconEntries]', N'U') IS NOT NULL DROP TABLE [BankLexiconEntries];");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddCharges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Charges')
                BEGIN
                    CREATE TABLE [Charges] (
                        [ChargeId]    UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        [TenantId]    UNIQUEIDENTIFIER NOT NULL,
                        [Title]       NVARCHAR(300)    NOT NULL,
                        [Description] NVARCHAR(MAX)    NULL,
                        [Amount]      DECIMAL(18,2)    NOT NULL,
                        [CreatedAt]   DATETIME2        NOT NULL DEFAULT SYSUTCDATETIME()
                    );
                END

                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ChargeCompanies')
                BEGIN
                    CREATE TABLE [ChargeCompanies] (
                        [ChargeId]   UNIQUEIDENTIFIER NOT NULL,
                        [CompanyId]  UNIQUEIDENTIFIER NOT NULL,
                        [Percentage] DECIMAL(5,2)     NOT NULL,
                        CONSTRAINT [PK_ChargeCompanies] PRIMARY KEY ([ChargeId], [CompanyId]),
                        CONSTRAINT [FK_ChargeCompanies_Charges] FOREIGN KEY ([ChargeId])
                            REFERENCES [Charges]([ChargeId]) ON DELETE CASCADE,
                        CONSTRAINT [FK_ChargeCompanies_Companies] FOREIGN KEY ([CompanyId])
                            REFERENCES [Companies]([CompanyId])
                    );
                END

                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ChargeDocuments')
                BEGIN
                    CREATE TABLE [ChargeDocuments] (
                        [ChargeDocumentId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        [ChargeId]         UNIQUEIDENTIFIER NOT NULL,
                        [FileName]         NVARCHAR(500)    NOT NULL,
                        [OriginalName]     NVARCHAR(500)    NOT NULL,
                        [UploadedAt]       DATETIME2        NOT NULL DEFAULT SYSUTCDATETIME(),
                        CONSTRAINT [FK_ChargeDocuments_Charges] FOREIGN KEY ([ChargeId])
                            REFERENCES [Charges]([ChargeId]) ON DELETE CASCADE
                    );
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ChargeDocuments')
                    DROP TABLE [ChargeDocuments];
                IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ChargeCompanies')
                    DROP TABLE [ChargeCompanies];
                IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Charges')
                    DROP TABLE [Charges];
            ");
        }
    }
}

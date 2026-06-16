using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class MigrateClientsToCompanyContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ── 1. Créer CompanyContacts ──────────────────────────────────────
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'CompanyContacts')
                BEGIN
                    CREATE TABLE [CompanyContacts] (
                        [ContactId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
                        [CompanyId] UNIQUEIDENTIFIER NOT NULL,
                        [TenantId]  UNIQUEIDENTIFIER NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000',
                        [Name]      NVARCHAR(200)    NOT NULL DEFAULT '',
                        [Mail]      NVARCHAR(200)    NULL,
                        [Phone]     NVARCHAR(50)     NULL,
                        [Notes]     NVARCHAR(MAX)    NULL,
                        CONSTRAINT [PK_CompanyContacts] PRIMARY KEY ([ContactId]),
                        CONSTRAINT [FK_CompanyContacts_Companies] FOREIGN KEY ([CompanyId])
                            REFERENCES [Companies] ([CompanyId]) ON DELETE CASCADE
                    );
                END
            ");

            // ── 2. Copier données existantes de Clients → CompanyContacts ─────
            //       En récupérant le TenantId depuis Companies
            migrationBuilder.Sql(@"
                INSERT INTO [CompanyContacts] ([ContactId], [CompanyId], [TenantId], [Name], [Mail], [Phone], [Notes])
                SELECT
                    NEWID(),
                    cl.[CompanyId],
                    ISNULL(co.[TenantId], '00000000-0000-0000-0000-000000000000'),
                    ISNULL(NULLIF(LTRIM(RTRIM(cl.[name])), ''), '(sans nom)'),
                    NULLIF(LTRIM(RTRIM(cl.[mail])),  ''),
                    NULLIF(LTRIM(RTRIM(cl.[phone])), ''),
                    NULL
                FROM [Clients] cl
                INNER JOIN [Companies] co ON cl.[CompanyId] = co.[CompanyId]
                WHERE cl.[name] IS NOT NULL
                  AND NOT EXISTS (
                      SELECT 1 FROM [CompanyContacts] cc
                      WHERE cc.[CompanyId] = cl.[CompanyId]
                  );
            ");

            // ── 3. Supprimer la table Clients ─────────────────────────────────
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Clients')
                BEGIN
                    DROP TABLE [Clients];
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recréer Clients (structure minimale)
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Clients')
                BEGIN
                    CREATE TABLE [Clients] (
                        [clientID]  UNIQUEIDENTIFIER NOT NULL,
                        [name]      NVARCHAR(MAX)    NOT NULL,
                        [mail]      NVARCHAR(MAX)    NULL,
                        [phone]     NVARCHAR(MAX)    NULL,
                        [notes]     NVARCHAR(MAX)    NULL,
                        [CompanyId] UNIQUEIDENTIFIER NOT NULL,
                        CONSTRAINT [PK_Clients] PRIMARY KEY ([clientID])
                    );
                END
            ");

            // Recopier depuis CompanyContacts → Clients
            migrationBuilder.Sql(@"
                INSERT INTO [Clients] ([clientID], [CompanyId], [name], [mail], [phone], [notes])
                SELECT [ContactId], [CompanyId], [Name], [Mail], [Phone], [Notes]
                FROM [CompanyContacts];
            ");

            // Supprimer CompanyContacts
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'CompanyContacts')
                    DROP TABLE [CompanyContacts];
            ");
        }
    }
}

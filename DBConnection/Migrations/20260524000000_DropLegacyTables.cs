using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class DropLegacyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop Works (FK to Companies and Employees)
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.Works', 'U') IS NOT NULL
                BEGIN
                    -- Drop FK constraints on Works first
                    DECLARE @sql NVARCHAR(MAX) = N'';
                    SELECT @sql += 'ALTER TABLE [dbo].[Works] DROP CONSTRAINT [' + fk.name + '];'
                    FROM sys.foreign_keys fk
                    WHERE fk.parent_object_id = OBJECT_ID('dbo.Works');
                    EXEC sp_executesql @sql;

                    DROP TABLE [dbo].[Works];
                END
            ");

            // 2. Drop MailCredentials (FK from Companies)
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.MailCredentials', 'U') IS NOT NULL
                BEGIN
                    -- Drop any FK on Companies referencing MailCredentials
                    DECLARE @sql NVARCHAR(MAX) = N'';
                    SELECT @sql += 'ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [' + fk.name + '];'
                    FROM sys.foreign_keys fk
                    JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                    WHERE fk.parent_object_id = OBJECT_ID('dbo.Companies')
                      AND fkc.referenced_object_id = OBJECT_ID('dbo.MailCredentials');
                    IF LEN(@sql) > 0 EXEC sp_executesql @sql;

                    DROP TABLE [dbo].[MailCredentials];
                END
            ");

            // 3. Drop WorkTypeId column from Companies (with its FK constraint)
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('dbo.Companies') AND name = 'WorkTypeId'
                )
                BEGIN
                    DECLARE @sql NVARCHAR(MAX) = N'';
                    SELECT @sql += 'ALTER TABLE [dbo].[Companies] DROP CONSTRAINT [' + fk.name + '];'
                    FROM sys.foreign_keys fk
                    JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                    JOIN sys.columns c ON fkc.parent_column_id = c.column_id
                        AND fkc.parent_object_id = c.object_id
                    WHERE fk.parent_object_id = OBJECT_ID('dbo.Companies')
                      AND c.name = 'WorkTypeId';
                    IF LEN(@sql) > 0 EXEC sp_executesql @sql;

                    ALTER TABLE [dbo].[Companies] DROP COLUMN [WorkTypeId];
                END
            ");

            // 4. Drop WorkTypes
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.WorkTypes', 'U') IS NOT NULL
                    DROP TABLE [dbo].[WorkTypes];
            ");

            // 5. Drop Providers
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.Providers', 'U') IS NOT NULL
                    DROP TABLE [dbo].[Providers];
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally not implemented — restoring dropped tables requires a full backup.
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddCredentialResetToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('EmployeeCredentials') AND name = 'ResetTokenHash'
                )
                BEGIN
                    ALTER TABLE [EmployeeCredentials] ADD [ResetTokenHash] nvarchar(128) NULL;
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('EmployeeCredentials') AND name = 'ResetTokenExpiresAt'
                )
                BEGIN
                    ALTER TABLE [EmployeeCredentials] ADD [ResetTokenExpiresAt] datetime2 NULL;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('EmployeeCredentials') AND name = 'ResetTokenExpiresAt'
                )
                BEGIN
                    ALTER TABLE [EmployeeCredentials] DROP COLUMN [ResetTokenExpiresAt];
                END

                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('EmployeeCredentials') AND name = 'ResetTokenHash'
                )
                BEGIN
                    ALTER TABLE [EmployeeCredentials] DROP COLUMN [ResetTokenHash];
                END
            ");
        }
    }
}

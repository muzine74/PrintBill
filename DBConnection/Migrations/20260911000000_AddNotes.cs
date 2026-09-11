using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Notes')
                BEGIN
                    CREATE TABLE [Notes] (
                        [NoteId]    UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        [TenantId]  UNIQUEIDENTIFIER NOT NULL,
                        [Text]      NVARCHAR(MAX)    NOT NULL,
                        [IsActive]  BIT              NOT NULL DEFAULT 1,
                        [CreatedAt] DATETIME2         NOT NULL DEFAULT SYSUTCDATETIME()
                    );
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Notes')
                BEGIN
                    DROP TABLE [Notes];
                END
            ");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddNoteIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'IsDeleted'
                )
                BEGIN
                    ALTER TABLE [Notes] ADD [IsDeleted] BIT NOT NULL DEFAULT 0;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'IsDeleted'
                )
                BEGIN
                    ALTER TABLE [Notes] DROP COLUMN [IsDeleted];
                END
            ");
        }
    }
}

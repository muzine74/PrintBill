using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddNoteLinkedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'LinkedEntityType'
                )
                BEGIN
                    ALTER TABLE [Notes] ADD [LinkedEntityType] NVARCHAR(20) NULL;
                END
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'LinkedEntityId'
                )
                BEGIN
                    ALTER TABLE [Notes] ADD [LinkedEntityId] UNIQUEIDENTIFIER NULL;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'LinkedEntityId'
                )
                BEGIN
                    ALTER TABLE [Notes] DROP COLUMN [LinkedEntityId];
                END
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'LinkedEntityType'
                )
                BEGIN
                    ALTER TABLE [Notes] DROP COLUMN [LinkedEntityType];
                END
            ");
        }
    }
}

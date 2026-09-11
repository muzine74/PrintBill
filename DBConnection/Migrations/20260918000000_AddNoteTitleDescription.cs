using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddNoteTitleDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'Text'
                ) AND NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'Title'
                )
                BEGIN
                    EXEC sp_rename 'Notes.Text', 'Title', 'COLUMN';
                END

                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'Description'
                )
                BEGIN
                    ALTER TABLE [Notes] ADD [Description] NVARCHAR(MAX) NOT NULL
                        CONSTRAINT DF_Notes_Description DEFAULT '';
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'Description'
                )
                BEGIN
                    ALTER TABLE [Notes] DROP CONSTRAINT DF_Notes_Description;
                    ALTER TABLE [Notes] DROP COLUMN [Description];
                END

                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'Title'
                ) AND NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'Text'
                )
                BEGIN
                    EXEC sp_rename 'Notes.Title', 'Text', 'COLUMN';
                END
            ");
        }
    }
}

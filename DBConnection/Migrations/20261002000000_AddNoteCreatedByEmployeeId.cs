using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddNoteCreatedByEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'CreatedByEmployeeId'
                )
                BEGIN
                    ALTER TABLE [Notes] ADD [CreatedByEmployeeId] UNIQUEIDENTIFIER NOT NULL
                        CONSTRAINT DF_Notes_CreatedByEmployeeId
                        DEFAULT '00000000-0000-0000-0000-000000000000';
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'Notes') AND name = N'CreatedByEmployeeId'
                )
                BEGIN
                    ALTER TABLE [Notes] DROP CONSTRAINT DF_Notes_CreatedByEmployeeId;
                    ALTER TABLE [Notes] DROP COLUMN [CreatedByEmployeeId];
                END
            ");
        }
    }
}

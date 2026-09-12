using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddChargeDocumentFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('Charges') AND name = 'DocumentFolder'
                )
                BEGIN
                    ALTER TABLE [Charges] ADD [DocumentFolder] nvarchar(260) NULL;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('Charges') AND name = 'DocumentFolder'
                )
                BEGIN
                    ALTER TABLE [Charges] DROP COLUMN [DocumentFolder];
                END
            ");
        }
    }
}

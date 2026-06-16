using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddContactIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('CompanyContacts') AND name = 'IsActive'
                )
                BEGIN
                    ALTER TABLE [CompanyContacts]
                    ADD [IsActive] bit NOT NULL DEFAULT 1;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE [CompanyContacts] DROP COLUMN [IsActive];
            ");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddPayerAccountNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'PayerAccountNumber'
                )
                BEGIN
                    ALTER TABLE [AppConfigs] ADD [PayerAccountNumber] nvarchar(30) NULL;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'PayerAccountNumber'
                )
                BEGIN
                    ALTER TABLE [AppConfigs] DROP COLUMN [PayerAccountNumber];
                END
            ");
        }
    }
}

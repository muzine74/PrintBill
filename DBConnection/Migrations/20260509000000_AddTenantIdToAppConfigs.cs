using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddTenantIdToAppConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'AppConfigs') AND name = N'TenantId'
                )
                BEGIN
                    ALTER TABLE [AppConfigs]
                    ADD [TenantId] UNIQUEIDENTIFIER NOT NULL
                        DEFAULT '00000000-0000-0000-0000-000000000001';
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID(N'AppConfigs') AND name = N'TenantId'
                )
                BEGIN
                    ALTER TABLE [AppConfigs] DROP COLUMN [TenantId];
                END
            ");
        }
    }
}

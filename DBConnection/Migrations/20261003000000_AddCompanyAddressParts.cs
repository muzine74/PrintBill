using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class AddCompanyAddressParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyCity')
                BEGIN
                    ALTER TABLE [AppConfigs] ADD [CompanyCity] nvarchar(max) NULL;
                END
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyProvince')
                BEGIN
                    ALTER TABLE [AppConfigs] ADD [CompanyProvince] nvarchar(max) NULL;
                END
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyPostalCode')
                BEGIN
                    ALTER TABLE [AppConfigs] ADD [CompanyPostalCode] nvarchar(max) NULL;
                END
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyCountry')
                BEGIN
                    ALTER TABLE [AppConfigs] ADD [CompanyCountry] nvarchar(max) NULL;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyCity')
                    ALTER TABLE [AppConfigs] DROP COLUMN [CompanyCity];
                IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyProvince')
                    ALTER TABLE [AppConfigs] DROP COLUMN [CompanyProvince];
                IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyPostalCode')
                    ALTER TABLE [AppConfigs] DROP COLUMN [CompanyPostalCode];
                IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AppConfigs') AND name = 'CompanyCountry')
                    ALTER TABLE [AppConfigs] DROP COLUMN [CompanyCountry];
            ");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    public partial class DropChargeOwnerCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Charges_OwnerCompany'
                )
                BEGIN
                    ALTER TABLE [Charges] DROP CONSTRAINT [FK_Charges_OwnerCompany];
                END

                IF EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('Charges') AND name = 'OwnerCompanyId'
                )
                BEGIN
                    ALTER TABLE [Charges] DROP COLUMN [OwnerCompanyId];
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('Charges') AND name = 'OwnerCompanyId'
                )
                BEGIN
                    ALTER TABLE [Charges] ADD [OwnerCompanyId] UNIQUEIDENTIFIER NULL;
                    ALTER TABLE [Charges] ADD CONSTRAINT [FK_Charges_OwnerCompany]
                        FOREIGN KEY ([OwnerCompanyId]) REFERENCES [Companies]([CompanyId]);
                END
            ");
        }
    }
}

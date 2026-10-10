using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Charges.TaxIncluded : le montant contient déjà les taxes (true) ou non (false, valeur des charges existantes).
    /// Ajout seul.</summary>
    public partial class AddChargeTaxIncluded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Charges', N'TaxIncluded') IS NULL
    ALTER TABLE [Charges] ADD [TaxIncluded] BIT NOT NULL CONSTRAINT [DF_Charges_TaxIncluded] DEFAULT 0;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Charges', N'TaxIncluded') IS NOT NULL
BEGIN
    ALTER TABLE [Charges] DROP CONSTRAINT [DF_Charges_TaxIncluded];
    ALTER TABLE [Charges] DROP COLUMN [TaxIncluded];
END
            ");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Charges.IsMonthly : charge mensuelle (true) ou ponctuelle (false, valeur des charges existantes).
    /// Ajout seul.</summary>
    public partial class AddChargeIsMonthly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Charges', N'IsMonthly') IS NULL
    ALTER TABLE [Charges] ADD [IsMonthly] BIT NOT NULL CONSTRAINT [DF_Charges_IsMonthly] DEFAULT 0;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Charges', N'IsMonthly') IS NOT NULL
BEGIN
    ALTER TABLE [Charges] DROP CONSTRAINT [DF_Charges_IsMonthly];
    ALTER TABLE [Charges] DROP COLUMN [IsMonthly];
END
            ");
        }
    }
}

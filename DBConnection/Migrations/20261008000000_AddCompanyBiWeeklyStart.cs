using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Companies.BiWeeklyStart : lundi de début de la « Semaine 1 » d'un planning bi-hebdomadaire.</summary>
    public partial class AddCompanyBiWeeklyStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Companies', N'BiWeeklyStart') IS NULL
                    ALTER TABLE [Companies] ADD [BiWeeklyStart] DATE NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Companies', N'BiWeeklyStart') IS NOT NULL
                    ALTER TABLE [Companies] DROP COLUMN [BiWeeklyStart];
            ");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Employees.UiTheme : design de couleurs choisi par l'utilisateur (NULL = défaut « nuit »).</summary>
    public partial class AddEmployeeUiTheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Employees', N'UiTheme') IS NULL
                    ALTER TABLE [Employees] ADD [UiTheme] NVARCHAR(20) NULL;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Employees', N'UiTheme') IS NOT NULL
                    ALTER TABLE [Employees] DROP COLUMN [UiTheme];
            ");
        }
    }
}

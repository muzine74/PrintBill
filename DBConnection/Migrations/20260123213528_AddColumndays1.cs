using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class AddColumndays1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPricingCalendar_Companies_CompanyId",
                table: "CompanyPricingCalendar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPricingCalendar",
                table: "CompanyPricingCalendar");

            migrationBuilder.RenameTable(
                name: "CompanyPricingCalendar",
                newName: "CompanyPricingCalendars");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPricingCalendar_CompanyId",
                table: "CompanyPricingCalendars",
                newName: "IX_CompanyPricingCalendars_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPricingCalendars",
                table: "CompanyPricingCalendars",
                column: "CompanyPricingCalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPricingCalendars_Companies_CompanyId",
                table: "CompanyPricingCalendars",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPricingCalendars_Companies_CompanyId",
                table: "CompanyPricingCalendars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPricingCalendars",
                table: "CompanyPricingCalendars");

            migrationBuilder.RenameTable(
                name: "CompanyPricingCalendars",
                newName: "CompanyPricingCalendar");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPricingCalendars_CompanyId",
                table: "CompanyPricingCalendar",
                newName: "IX_CompanyPricingCalendar_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPricingCalendar",
                table: "CompanyPricingCalendar",
                column: "CompanyPricingCalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPricingCalendar_Companies_CompanyId",
                table: "CompanyPricingCalendar",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

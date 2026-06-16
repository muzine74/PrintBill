#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class AddEmployeeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name:         "EmployeeType",
                table:        "Employees",
                type:         "nvarchar(20)",
                maxLength:    20,
                nullable:     false,
                defaultValue: "Permanent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name:  "EmployeeType",
                table: "Employees");
        }
    }
}

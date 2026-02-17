using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class changeemployeetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Employees",
                newName: "EmployeePhone");

            migrationBuilder.RenameColumn(
                name: "mail",
                table: "Employees",
                newName: "EmployeeMail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeePhone",
                table: "Employees",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "EmployeeMail",
                table: "Employees",
                newName: "mail");
        }
    }
}

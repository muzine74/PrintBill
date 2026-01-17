using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class CreationDesEntityWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorks_WorkTypes_WorkTypeId",
                table: "CompanyWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Employees_EmployeeId",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "clientID",
                table: "Clients",
                newName: "ClientID");

            migrationBuilder.AlterColumn<string>(
                name: "Workdate",
                table: "Works",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeWorks",
                columns: table => new
                {
                    EmployeeWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoursWorked = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorks", x => x.EmployeeWorkId);
                    table.CheckConstraint("CK_EmployeeWork_Amount", "[Amount] >= 0");
                    table.CheckConstraint("CK_EmployeeWork_HoursWorked", "[HoursWorked] > 0");
                    table.CheckConstraint("CK_EmployeeWork_Status", "[Status] IN (0, 1, 2, 3, 4)");
                    table.ForeignKey(
                        name: "FK_EmployeeWorks_CompanyWorks_CompanyWorkId",
                        column: x => x.CompanyWorkId,
                        principalTable: "CompanyWorks",
                        principalColumn: "CompanyWorkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeWorks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_DayOfWeek",
                table: "WorkSchedules",
                column: "DayOfWeek");

            migrationBuilder.CreateIndex(
                name: "IX_Works_Workdate",
                table: "Works",
                column: "Workdate");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorks_CompanyWorkId",
                table: "EmployeeWorks",
                column: "CompanyWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorks_EmployeeId",
                table: "EmployeeWorks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorks_EmployeeId_WorkDate",
                table: "EmployeeWorks",
                columns: new[] { "EmployeeId", "WorkDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorks_Status",
                table: "EmployeeWorks",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorks_WorkDate",
                table: "EmployeeWorks",
                column: "WorkDate");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorks_WorkTypes_WorkTypeId",
                table: "CompanyWorks",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "WorkTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Employees_EmployeeId",
                table: "Works",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorks_WorkTypes_WorkTypeId",
                table: "CompanyWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Employees_EmployeeId",
                table: "Works");

            migrationBuilder.DropTable(
                name: "EmployeeWorks");

            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_DayOfWeek",
                table: "WorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Works_Workdate",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "Clients",
                newName: "clientID");

            migrationBuilder.AlterColumn<string>(
                name: "Workdate",
                table: "Works",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorks_WorkTypes_WorkTypeId",
                table: "CompanyWorks",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "WorkTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Employees_EmployeeId",
                table: "Works",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

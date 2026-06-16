using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class ajoutdetablelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeTimeLogs",
                columns: table => new
                {
                    EmployeeTimeLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkType1 = table.Column<int>(type: "int", nullable: false),
                    Workdate = table.Column<DateOnly>(type: "date", nullable: false),
                    ClientPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeginWorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndWorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTimeLogs", x => x.EmployeeTimeLogId);
                    table.ForeignKey(
                        name: "FK_EmployeeTimeLogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTimeLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTimeLogs_CompanyId",
                table: "EmployeeTimeLogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTimeLogs_EmployeeId",
                table: "EmployeeTimeLogs",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTimeLogs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class addfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFiles",
                columns: table => new
                {
                    EmployeeFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId     = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName       = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OriginalName   = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedAt     = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFiles", x => x.EmployeeFileId);
                    table.ForeignKey(
                        name: "FK_EmployeeFiles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFiles_EmployeeId",
                table: "EmployeeFiles",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "EmployeeFiles");
        }
    }
}

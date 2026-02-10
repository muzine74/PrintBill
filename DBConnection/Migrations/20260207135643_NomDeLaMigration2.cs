using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class NomDeLaMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicatedDate",
                table: "EmployeeCompagnyPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicatedDate",
                table: "EmployeeCompagnyPricings");
        }
    }
}

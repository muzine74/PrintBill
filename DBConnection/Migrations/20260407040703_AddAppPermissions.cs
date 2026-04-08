using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class AddAppPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermissions", x => x.PermissionId);
                });

            migrationBuilder.InsertData(
                table: "AppPermissions",
                columns: new[] { "PermissionId", "Key", "Label", "Module" },
                values: new object[,]
                {
                    { 1, "employees.view", "Voir", "Employés" },
                    { 2, "employees.edit", "Modifier", "Employés" },
                    { 3, "employees.create", "Créer", "Employés" },
                    { 4, "employees.delete", "Supprimer", "Employés" },
                    { 5, "companies.view", "Voir", "Compagnies" },
                    { 6, "companies.edit", "Modifier", "Compagnies" },
                    { 7, "invoices.view", "Voir", "Factures" },
                    { 8, "invoices.edit", "Modifier", "Factures" },
                    { 9, "invoices.send", "Envoyer", "Factures" },
                    { 10, "pointage.view", "Voir", "Pointage" },
                    { 11, "pointage.validate", "Valider", "Pointage" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPermissions_Key",
                table: "AppPermissions",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPermissions");
        }
    }
}

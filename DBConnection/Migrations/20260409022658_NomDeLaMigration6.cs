using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class NomDeLaMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM [AppPermissions] WHERE [Key] = N'groups.manage')
                BEGIN
                    SET IDENTITY_INSERT [AppPermissions] ON;
                    INSERT INTO [AppPermissions] ([PermissionId], [Key], [Label], [Module])
                    VALUES (1001, N'groups.manage', N'Gérer', N'Groupes');
                    SET IDENTITY_INSERT [AppPermissions] OFF;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppPermissions",
                keyColumn: "PermissionId",
                keyValue: 1001);
        }
    }
}

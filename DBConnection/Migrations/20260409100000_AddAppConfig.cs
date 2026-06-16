using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class AddAppConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    ConfigId       = table.Column<int>(type: "int", nullable: false),
                    LogoPath       = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName    = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CompanyAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyPhone   = table.Column<string>(type: "nvarchar(50)",  maxLength: 50,  nullable: true),
                    CompanyEmail   = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SmtpServer     = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SmtpPort       = table.Column<int>(type: "int", nullable: true),
                    SmtpUser       = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SmtpPassword   = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TpsNumber      = table.Column<string>(type: "nvarchar(50)",  maxLength: 50,  nullable: true),
                    TvqNumber      = table.Column<string>(type: "nvarchar(50)",  maxLength: 50,  nullable: true),
                    TpsRate        = table.Column<decimal>(type: "decimal(6,4)", nullable: false, defaultValue: 5m),
                    TvqRate        = table.Column<decimal>(type: "decimal(6,4)", nullable: false, defaultValue: 9.975m),
                    BankCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName    = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPhone   = table.Column<string>(type: "nvarchar(50)",  maxLength: 50,  nullable: true),
                    ContactEmail   = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AppVersion     = table.Column<string>(type: "nvarchar(50)",  maxLength: 50,  nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.ConfigId);
                });

            // Ligne singleton
            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "ConfigId", "TpsRate", "TvqRate" },
                values: new object[] { 1, 5m, 9.975m });

            // Permission config.manage
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM [AppPermissions] WHERE [Key] = N'config.manage')
                BEGIN
                    SET IDENTITY_INSERT [AppPermissions] ON;
                    INSERT INTO [AppPermissions] ([PermissionId], [Key], [Label], [Module])
                    VALUES (1002, N'config.manage', N'Configurer', N'Application');
                    SET IDENTITY_INSERT [AppPermissions] OFF;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AppConfigs");
            migrationBuilder.DeleteData(table: "AppPermissions", keyColumn: "PermissionId", keyValue: 1002);
        }
    }
}

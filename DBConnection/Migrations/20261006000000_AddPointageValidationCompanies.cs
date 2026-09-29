using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>
    /// Photo figée des compagnies de l'employé au moment de la validation d'une semaine.
    /// Semaines déjà validées : reconstituée à partir des pointages de la semaine (jamais depuis
    /// l'affectation actuelle, qui a pu changer depuis).
    /// </summary>
    public partial class AddPointageValidationCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID(N'PointageValidationCompanies') IS NULL
                BEGIN
                    CREATE TABLE [PointageValidationCompanies] (
                        [PointageValidationId] UNIQUEIDENTIFIER NOT NULL
                            CONSTRAINT FK_PointageValidationCompanies_PointageValidations
                            REFERENCES [PointageValidations]([PointageValidationId]) ON DELETE CASCADE,
                        [CompanyId]   UNIQUEIDENTIFIER NOT NULL,
                        [CompanyName] NVARCHAR(MAX)    NOT NULL,
                        [Note]        NVARCHAR(MAX)    NULL,
                        CONSTRAINT PK_PointageValidationCompanies PRIMARY KEY ([PointageValidationId], [CompanyId])
                    );
                END
            ");
            migrationBuilder.Sql(@"
                INSERT INTO [PointageValidationCompanies] ([PointageValidationId], [CompanyId], [CompanyName], [Note])
                SELECT DISTINCT v.[PointageValidationId], l.[CompanyId], ISNULL(c.[companyName], N''), ec.[Note]
                FROM [PointageValidations] v
                JOIN [EmployeeTimeLogs] l
                  ON l.[EmployeeId] = v.[EmployeeId]
                 AND l.[TenantId]   = v.[TenantId]
                 AND l.[Workdate] >= v.[WeekStart]
                 AND l.[Workdate] <= DATEADD(DAY, 6, v.[WeekStart])
                LEFT JOIN [Companies] c ON c.[CompanyId] = l.[CompanyId]
                LEFT JOIN [EmployeeCompanies] ec ON ec.[EmployeeId] = v.[EmployeeId] AND ec.[CompanyId] = l.[CompanyId]
                WHERE NOT EXISTS (SELECT 1 FROM [PointageValidationCompanies] p
                                  WHERE p.[PointageValidationId] = v.[PointageValidationId]
                                    AND p.[CompanyId] = l.[CompanyId]);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'PointageValidationCompanies') IS NOT NULL DROP TABLE [PointageValidationCompanies];");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>
    /// Semaines validées avant l'introduction de la photo figée (2026-09-27 22:15 UTC) : ajoute à leur
    /// photo les compagnies actives affectées sans visite (aucun historique daté des affectations
    /// n'existe ; l'affectation au moment de la migration est la meilleure référence). Une seule fois.
    /// </summary>
    public partial class BackfillValidationAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [PointageValidationCompanies] ([PointageValidationId], [CompanyId], [CompanyName], [Note])
                SELECT v.[PointageValidationId], ec.[CompanyId], c.[companyName], ec.[Note]
                FROM [PointageValidations] v
                JOIN [EmployeeCompanies] ec ON ec.[EmployeeId] = v.[EmployeeId]
                JOIN [Companies] c          ON c.[CompanyId]   = ec.[CompanyId]
                                           AND c.[TenantId]    = v.[TenantId]
                                           AND c.[companyStatus] = 1
                WHERE v.[ValidatedAt] < '2026-09-27T22:15:00'
                  AND NOT EXISTS (SELECT 1 FROM [PointageValidationCompanies] p
                                  WHERE p.[PointageValidationId] = v.[PointageValidationId]
                                    AND p.[CompanyId] = ec.[CompanyId]);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}

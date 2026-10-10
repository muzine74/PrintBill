using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>EmployeePayments : l'index unique (TenantId, EmployeeId, PeriodStart, PeriodEnd) devient non unique,
    /// pour autoriser plusieurs versements sur la même semaine. Aucune donnée modifiée.</summary>
    public partial class AllowEmployeePaymentInstallments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- L'index unique (un seul paiement par employé et par période) devient un index simple.
DECLARE @ix SYSNAME = (
    SELECT TOP 1 i.name FROM sys.indexes i
    WHERE i.object_id = OBJECT_ID(N'[EmployeePayments]') AND i.is_unique = 1 AND i.is_primary_key = 0
      AND (SELECT COUNT(*) FROM sys.index_columns ic WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 0) = 4
      AND NOT EXISTS (
            SELECT 1 FROM sys.index_columns ic
            JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
            WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 0
              AND c.name NOT IN (N'TenantId', N'EmployeeId', N'PeriodStart', N'PeriodEnd')));
IF @ix IS NOT NULL
BEGIN
    DECLARE @sql NVARCHAR(MAX) = N'DROP INDEX ' + QUOTENAME(@ix) + N' ON [EmployeePayments];';
    EXEC sp_executesql @sql;
END
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'[EmployeePayments]') AND name = N'IX_EmployeePayments_Tenant_Employee_Period')
    CREATE INDEX [IX_EmployeePayments_Tenant_Employee_Period]
        ON [EmployeePayments] ([TenantId], [EmployeeId], [PeriodStart], [PeriodEnd]);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Retour à l'index unique : impossible tant qu'une semaine a plusieurs versements.
            migrationBuilder.Sql(@"
DROP INDEX [IX_EmployeePayments_Tenant_Employee_Period] ON [EmployeePayments];
CREATE UNIQUE INDEX [IX_EmployeePayments_Tenant_Employee_Period]
    ON [EmployeePayments] ([TenantId], [EmployeeId], [PeriodStart], [PeriodEnd]);
            ");
        }
    }
}

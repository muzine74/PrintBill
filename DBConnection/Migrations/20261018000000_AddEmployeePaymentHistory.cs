using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Table EmployeePaymentHistories : une ligne par enregistrement d'un paiement employé
    /// (date, utilisateur, ancien / nouveau montant, écart, journées, raison). Création seule.</summary>
    public partial class AddEmployeePaymentHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[EmployeePaymentHistories]', N'U') IS NULL
BEGIN
    CREATE TABLE [EmployeePaymentHistories] (
        [EmployeePaymentHistoryId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_EmployeePaymentHistories] PRIMARY KEY,
        [TenantId]       UNIQUEIDENTIFIER NOT NULL,
        [EmployeeId]     UNIQUEIDENTIFIER NOT NULL,
        [PeriodStart]    DATE             NOT NULL,
        [PeriodEnd]      DATE             NOT NULL,
        [ChangedAt]      DATETIME2        NOT NULL,
        [ChangedBy]      NVARCHAR(256)    NULL,
        [PreviousAmount] DECIMAL(18,2)    NULL,
        [NewAmount]      DECIMAL(18,2)    NOT NULL,
        [Delta]          DECIMAL(18,2)    NOT NULL,
        [PaidDates]      NVARCHAR(4000)   NULL,
        [Note]           NVARCHAR(1000)   NULL,
        CONSTRAINT [FK_EmployeePaymentHistories_Employees_EmployeeId]
            FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId])
    );
    CREATE INDEX [IX_EmployeePaymentHistories_TenantId_EmployeeId_PeriodStart_PeriodEnd]
        ON [EmployeePaymentHistories] ([TenantId], [EmployeeId], [PeriodStart], [PeriodEnd]);
    CREATE INDEX [IX_EmployeePaymentHistories_EmployeeId] ON [EmployeePaymentHistories] ([EmployeeId]);
END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'[EmployeePaymentHistories]', N'U') IS NOT NULL DROP TABLE [EmployeePaymentHistories];");
        }
    }
}

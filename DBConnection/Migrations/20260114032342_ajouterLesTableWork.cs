using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBConnection.Migrations
{
    /// <inheritdoc />
    public partial class ajouterLesTableWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    civicNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    suite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "BillHistories",
                columns: table => new
                {
                    billIdentifier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    compagnyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    compagnyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MouthBill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BilledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillDescriptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    compagnyPrice = table.Column<float>(type: "real", nullable: false),
                    NumberOfVisite = table.Column<int>(type: "int", nullable: false),
                    TotalWithOutTax = table.Column<float>(type: "real", nullable: false),
                    TPS = table.Column<float>(type: "real", nullable: false),
                    TVQ = table.Column<float>(type: "real", nullable: false),
                    TotalWithTax = table.Column<float>(type: "real", nullable: false),
                    BillPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterSendedBillPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issended = table.Column<bool>(type: "bit", nullable: true),
                    IsPayed = table.Column<bool>(type: "bit", nullable: true),
                    BillHistoryNote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillHistories", x => x.billIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyStatus = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prividercode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdMailCredentiel = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TPSNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TVQNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    providerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.providerID);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    WorkTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.WorkTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BillDescriptions",
                columns: table => new
                {
                    BillDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillHistoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: false),
                    SubTotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDescriptions", x => x.BillDescriptionId);
                    table.ForeignKey(
                        name: "FK_BillDescriptions_BillHistories_BillHistoryId",
                        column: x => x.BillHistoryId,
                        principalTable: "BillHistories",
                        principalColumn: "billIdentifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    clientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.clientID);
                    table.ForeignKey(
                        name: "FK_Clients_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAddresses",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAddresses", x => new { x.CompanyId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_CompanyAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAddresses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailCredentials",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    smtpServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    smtpPort = table.Column<int>(type: "int", nullable: true),
                    smtpUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    smtpPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailCredentials", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_MailCredentials_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAddresses",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddresses", x => new { x.EmployeeId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_EmployeeAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAddresses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCompanies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCompanies", x => new { x.EmployeeId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_EmployeeCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCompanies_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkType1 = table.Column<int>(type: "int", nullable: false),
                    Workdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeginWorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndWorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.WorkId);
                    table.ForeignKey(
                        name: "FK_Works_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Works_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWorks",
                columns: table => new
                {
                    CompanyWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeeklyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MonthlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorks", x => x.CompanyWorkId);
                    table.ForeignKey(
                        name: "FK_CompanyWorks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyWorks_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "WorkTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCompanyWorks",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCompanyWorks", x => new { x.EmployeeId, x.CompanyWorkId });
                    table.ForeignKey(
                        name: "FK_EmployeeCompanyWorks_CompanyWorks_CompanyWorkId",
                        column: x => x.CompanyWorkId,
                        principalTable: "CompanyWorks",
                        principalColumn: "CompanyWorkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCompanyWorks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHours",
                columns: table => new
                {
                    WorkHourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHours", x => x.WorkHourId);
                    table.ForeignKey(
                        name: "FK_WorkHours_CompanyWorks_CompanyWorkId",
                        column: x => x.CompanyWorkId,
                        principalTable: "CompanyWorks",
                        principalColumn: "CompanyWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    WorkScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<byte>(type: "tinyint", nullable: false),
                    DayOfMonth = table.Column<byte>(type: "tinyint", nullable: true),
                    WeekCycle = table.Column<byte>(type: "tinyint", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.WorkScheduleId);
                    table.ForeignKey(
                        name: "FK_WorkSchedules_CompanyWorks_CompanyWorkId",
                        column: x => x.CompanyWorkId,
                        principalTable: "CompanyWorks",
                        principalColumn: "CompanyWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkTypes",
                columns: new[] { "WorkTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Par visite" },
                    { 2, "Hebdomadaire" },
                    { 3, "Bi-hebdomadaire" },
                    { 4, "Bi-mensuel" },
                    { 5, "Mensuel" },
                    { 6, "Horaire" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDescriptions_BillHistoryId",
                table: "BillDescriptions",
                column: "BillHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BillHistories_Id",
                table: "BillHistories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyId",
                table: "Clients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddresses_AddressId",
                table: "CompanyAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorks_CompanyId",
                table: "CompanyWorks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorks_WorkTypeId",
                table: "CompanyWorks",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAddresses_AddressId",
                table: "EmployeeAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompanies_CompanyId",
                table: "EmployeeCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompanyWorks_CompanyWorkId",
                table: "EmployeeCompanyWorks",
                column: "CompanyWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_MailCredentials_CompanyId",
                table: "MailCredentials",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_CompanyWorkId",
                table: "WorkHours",
                column: "CompanyWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_CompanyId",
                table: "Works",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_EmployeeId",
                table: "Works",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_CompanyWorkId",
                table: "WorkSchedules",
                column: "CompanyWorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDescriptions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "CompanyAddresses");

            migrationBuilder.DropTable(
                name: "EmployeeAddresses");

            migrationBuilder.DropTable(
                name: "EmployeeCompanies");

            migrationBuilder.DropTable(
                name: "EmployeeCompanyWorks");

            migrationBuilder.DropTable(
                name: "MailCredentials");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "WorkHours");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "BillHistories");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "CompanyWorks");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "WorkTypes");
        }
    }
}

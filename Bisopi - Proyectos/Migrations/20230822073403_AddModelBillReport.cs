using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddModelBillReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceReports",
                columns: table => new
                {
                    InvoiceReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsItChangeControl = table.Column<bool>(type: "bit", nullable: false),
                    MilestoneNumber = table.Column<int>(type: "int", nullable: false),
                    MilestoneDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusBill = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true),
                    BillingApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BilledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceReports", x => x.InvoiceReportID);
                    table.ForeignKey(
                        name: "FK_InvoiceReports_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceReports_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceReports_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceReports_ProjectsStatus_ProjectStatusID",
                        column: x => x.ProjectStatusID,
                        principalTable: "ProjectsStatus",
                        principalColumn: "ProjectStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReports_ClientID",
                table: "InvoiceReports",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReports_CountryID",
                table: "InvoiceReports",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReports_CurrencyID",
                table: "InvoiceReports",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReports_ProjectStatusID",
                table: "InvoiceReports",
                column: "ProjectStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceReports");
        }
    }
}

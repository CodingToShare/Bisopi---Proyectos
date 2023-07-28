using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class initialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Abbreviation = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Abbreviation = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyName = table.Column<string>(type: "varchar(200)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsStatus",
                columns: table => new
                {
                    ProjectStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectStatusName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Abbreviation = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsStatus", x => x.ProjectStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsTypes",
                columns: table => new
                {
                    ProjectTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTypeName = table.Column<string>(type: "varchar(200)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsTypes", x => x.ProjectTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SupportsStatus",
                columns: table => new
                {
                    SupportStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupportStatusName = table.Column<string>(type: "varchar(200)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportsStatus", x => x.SupportStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "varchar(200)", nullable: false),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerManager = table.Column<string>(type: "varchar(500)", nullable: false),
                    LeaderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupportStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstimateRequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnswerDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestPriority = table.Column<string>(type: "varchar(200)", nullable: false),
                    ScalerCode = table.Column<string>(type: "varchar(200)", nullable: true),
                    ClarityCode = table.Column<string>(type: "varchar(200)", nullable: true),
                    EstimatedHours = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectValue = table.Column<double>(type: "float", nullable: false),
                    ProjectCost = table.Column<double>(type: "float", nullable: false),
                    TRMProject = table.Column<double>(type: "float", nullable: false),
                    GrossMargin = table.Column<double>(type: "float", nullable: false),
                    Billable = table.Column<bool>(type: "bit", nullable: false),
                    Justification = table.Column<string>(type: "varchar(1000)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectsStatus_ProjectStatusID",
                        column: x => x.ProjectStatusID,
                        principalTable: "ProjectsStatus",
                        principalColumn: "ProjectStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectsTypes_ProjectTypeID",
                        column: x => x.ProjectTypeID,
                        principalTable: "ProjectsTypes",
                        principalColumn: "ProjectTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_SupportsStatus_SupportStatusID",
                        column: x => x.SupportStatusID,
                        principalTable: "SupportsStatus",
                        principalColumn: "SupportStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientID",
                table: "Projects",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CountryID",
                table: "Projects",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CurrencyID",
                table: "Projects",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectStatusID",
                table: "Projects",
                column: "ProjectStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeID",
                table: "Projects",
                column: "ProjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SupportStatusID",
                table: "Projects",
                column: "SupportStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "ProjectsStatus");

            migrationBuilder.DropTable(
                name: "ProjectsTypes");

            migrationBuilder.DropTable(
                name: "SupportsStatus");
        }
    }
}

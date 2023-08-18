using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddModelResourcePlanings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourcesPlannings",
                columns: table => new
                {
                    ResourcePlanningID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeadID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlannedHours = table.Column<double>(type: "float", nullable: false),
                    EtcHour = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesPlannings", x => x.ResourcePlanningID);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesPlanningsTemps",
                columns: table => new
                {
                    ResourcePlanningTempID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeadID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlannedHours = table.Column<double>(type: "float", nullable: false),
                    EtcHour = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesPlanningsTemps", x => x.ResourcePlanningTempID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourcesPlannings");

            migrationBuilder.DropTable(
                name: "ResourcesPlanningsTemps");
        }
    }
}

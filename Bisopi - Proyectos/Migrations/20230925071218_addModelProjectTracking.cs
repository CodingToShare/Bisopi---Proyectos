using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class addModelProjectTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTrackings",
                columns: table => new
                {
                    ProjectTrackingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MonitoringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedPercentage = table.Column<double>(type: "float", nullable: false),
                    RealPercentage = table.Column<double>(type: "float", nullable: false),
                    Comments = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTrackings", x => x.ProjectTrackingID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTrackings");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarHitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    MilestoneID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeadID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MilestoneDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true),
                    MilestoneNumber = table.Column<int>(type: "int", nullable: false),
                    IsItChangeControl = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.MilestoneID);
                });

            migrationBuilder.CreateTable(
                name: "MilestonesTemps",
                columns: table => new
                {
                    MilestoneTempID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DealID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeadID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MilestoneDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true),
                    MilestoneNumber = table.Column<int>(type: "int", nullable: false),
                    IsItChangeControl = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestonesTemps", x => x.MilestoneTempID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "MilestonesTemps");
        }
    }
}

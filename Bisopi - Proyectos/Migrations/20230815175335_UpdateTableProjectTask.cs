using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProjectTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimateTimeMinutes",
                table: "ProjectTask",
                newName: "ExecutionTime");

            migrationBuilder.RenameColumn(
                name: "EstimateTimeHours",
                table: "ProjectTask",
                newName: "EstimateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExecutionTime",
                table: "ProjectTask",
                newName: "EstimateTimeMinutes");

            migrationBuilder.RenameColumn(
                name: "EstimateTime",
                table: "ProjectTask",
                newName: "EstimateTimeHours");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class EditModelProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstimatedHoursLineBase",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProjectValueLineBase",
                table: "Projects",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedHoursLineBase",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectValueLineBase",
                table: "Projects");
        }
    }
}

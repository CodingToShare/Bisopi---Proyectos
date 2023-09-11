using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Seniorities_SeniorityID",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_SeniorityID",
                table: "Costs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Costs_SeniorityID",
                table: "Costs",
                column: "SeniorityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Seniorities_SeniorityID",
                table: "Costs",
                column: "SeniorityID",
                principalTable: "Seniorities",
                principalColumn: "SeniorityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

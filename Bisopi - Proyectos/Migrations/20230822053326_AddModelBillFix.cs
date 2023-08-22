using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddModelBillFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Milestones_MilestoneID",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_MilestoneID",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bills_MilestoneID",
                table: "Bills",
                column: "MilestoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Milestones_MilestoneID",
                table: "Bills",
                column: "MilestoneID",
                principalTable: "Milestones",
                principalColumn: "MilestoneID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

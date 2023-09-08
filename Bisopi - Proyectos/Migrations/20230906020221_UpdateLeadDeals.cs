using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLeadDeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedPresaleID",
                table: "Leads",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Billable",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PlannedMilestones",
                table: "Leads",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProposedDeliveryDate",
                table: "Leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedPresaleID",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Billable",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "PlannedMilestones",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ProposedDeliveryDate",
                table: "Leads");
        }
    }
}

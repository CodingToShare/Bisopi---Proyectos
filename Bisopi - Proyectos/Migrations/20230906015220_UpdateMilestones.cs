using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMilestones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "ResourcesPlanningsTemps",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fee",
                table: "ResourcesPlanningsTemps",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "ResourcesPlannings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fee",
                table: "ResourcesPlannings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedPresaleID",
                table: "Deals",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Billable",
                table: "Deals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PlannedMilestones",
                table: "Deals",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProposedDeliveryDate",
                table: "Deals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ResourcesPlanningsTemps");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "ResourcesPlanningsTemps");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ResourcesPlannings");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "ResourcesPlannings");

            migrationBuilder.DropColumn(
                name: "AssignedPresaleID",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Billable",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "PlannedMilestones",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ProposedDeliveryDate",
                table: "Deals");
        }
    }
}

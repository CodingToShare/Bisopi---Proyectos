using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResourcePlaning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SeniorityID",
                table: "ResourcesPlanningsTemps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SeniorityID",
                table: "ResourcesPlannings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SeniorityID",
                table: "ResourcePlanningReals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeniorityID",
                table: "ResourcesPlanningsTemps");

            migrationBuilder.DropColumn(
                name: "SeniorityID",
                table: "ResourcesPlannings");

            migrationBuilder.DropColumn(
                name: "SeniorityID",
                table: "ResourcePlanningReals");
        }
    }
}

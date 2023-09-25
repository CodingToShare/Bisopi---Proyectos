using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class EditModelLEadDeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ResourcesPlannings",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ResourcesPlannings",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<double>(
                name: "CommercialDiscount",
                table: "Leads",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GrossMargin",
                table: "Leads",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProjectCost",
                table: "Leads",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalHours",
                table: "Leads",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CommercialDiscount",
                table: "Deals",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GrossMargin",
                table: "Deals",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProjectCost",
                table: "Deals",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalHours",
                table: "Deals",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommercialDiscount",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "GrossMargin",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ProjectCost",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CommercialDiscount",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "GrossMargin",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ProjectCost",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Deals");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ResourcesPlannings",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ResourcesPlannings",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}

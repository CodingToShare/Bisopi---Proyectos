using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class TestMillestone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceReports_Countries_CountryID",
                table: "InvoiceReports");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceReports_CountryID",
                table: "InvoiceReports");

            migrationBuilder.AlterColumn<int>(
                name: "MilestoneNumber",
                table: "InvoiceReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                table: "InvoiceReports",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MilestoneNumber",
                table: "InvoiceReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                table: "InvoiceReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReports_CountryID",
                table: "InvoiceReports",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceReports_Countries_CountryID",
                table: "InvoiceReports",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

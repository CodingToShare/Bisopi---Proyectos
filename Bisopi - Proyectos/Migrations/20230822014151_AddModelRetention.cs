using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddModelRetention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetentionPercentages",
                columns: table => new
                {
                    RetentionPercentageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Retention = table.Column<double>(type: "float", nullable: false),
                    ValueAddedTax = table.Column<double>(type: "float", nullable: false),
                    ValueAddedTaxWuthholding = table.Column<double>(type: "float", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionPercentages", x => x.RetentionPercentageID);
                    table.ForeignKey(
                        name: "FK_RetentionPercentages_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RetentionPercentages_CountryID",
                table: "RetentionPercentages",
                column: "CountryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetentionPercentages");
        }
    }
}

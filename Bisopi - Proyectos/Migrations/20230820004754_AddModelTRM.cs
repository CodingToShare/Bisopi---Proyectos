using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddModelTRM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepresentativeMarketRates",
                columns: table => new
                {
                    RepresentativeMarketRateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectedRm = table.Column<double>(type: "float", nullable: false),
                    CurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentativeMarketRates", x => x.RepresentativeMarketRateID);
                    table.ForeignKey(
                        name: "FK_RepresentativeMarketRates_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepresentativeMarketRates_CurrencyID",
                table: "RepresentativeMarketRates",
                column: "CurrencyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepresentativeMarketRates");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddModelBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MilestoneID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusBill = table.Column<int>(type: "int", nullable: false),
                    InvoiceShipmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuedDocument = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    RelatedDocument = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    NoteInvoice = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    InvoiceData = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    ConceptInvoice = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    ProposalDocument = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_Bills_Milestones_MilestoneID",
                        column: x => x.MilestoneID,
                        principalTable: "Milestones",
                        principalColumn: "MilestoneID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_MilestoneID",
                table: "Bills",
                column: "MilestoneID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}

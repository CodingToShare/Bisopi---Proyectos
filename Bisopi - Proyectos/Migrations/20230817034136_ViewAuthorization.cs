using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class ViewAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllowedViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllowedViewsForGroups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedViewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedViewsForGroups", x => new { x.GroupId, x.AllowedViewId });
                    table.ForeignKey(
                        name: "FK_AllowedViewsForGroups_AllowedViews_AllowedViewId",
                        column: x => x.AllowedViewId,
                        principalTable: "AllowedViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowedViewsForRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedViewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedViewsForRoles", x => new { x.RoleId, x.AllowedViewId });
                    table.ForeignKey(
                        name: "FK_AllowedViewsForRoles_AllowedViews_AllowedViewId",
                        column: x => x.AllowedViewId,
                        principalTable: "AllowedViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedViewsForGroups_AllowedViewId",
                table: "AllowedViewsForGroups",
                column: "AllowedViewId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowedViewsForRoles_AllowedViewId",
                table: "AllowedViewsForRoles",
                column: "AllowedViewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedViewsForGroups");

            migrationBuilder.DropTable(
                name: "AllowedViewsForRoles");

            migrationBuilder.DropTable(
                name: "AllowedViews");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProjectTaskAndCreateTableProjectTaskStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectTask");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskStatusID",
                table: "ProjectTask",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProjectTaskStatus",
                columns: table => new
                {
                    ProjectTaskStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskStatus", x => x.ProjectTaskStatusID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_TaskStatusID",
                table: "ProjectTask",
                column: "TaskStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_ProjectTaskStatus_TaskStatusID",
                table: "ProjectTask",
                column: "TaskStatusID",
                principalTable: "ProjectTaskStatus",
                principalColumn: "ProjectTaskStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_ProjectTaskStatus_TaskStatusID",
                table: "ProjectTask");

            migrationBuilder.DropTable(
                name: "ProjectTaskStatus");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTask_TaskStatusID",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "TaskStatusID",
                table: "ProjectTask");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ProjectTask",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

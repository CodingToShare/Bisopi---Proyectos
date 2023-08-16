using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddTableProjectTaskRegistry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskRegistry",
                columns: table => new
                {
                    ProjectTaskRegistryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: false),
                    McaExecution = table.Column<bool>(type: "bit", nullable: true),
                    McaManual = table.Column<bool>(type: "bit", nullable: true),
                    McaHistorico = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskRegistry", x => x.ProjectTaskRegistryID);
                    table.ForeignKey(
                        name: "FK_TaskRegistry_ProjectTask_ProjectTaskID",
                        column: x => x.ProjectTaskID,
                        principalTable: "ProjectTask",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskRegistry_ProjectTaskID",
                table: "TaskRegistry",
                column: "ProjectTaskID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskRegistry");
        }
    }
}

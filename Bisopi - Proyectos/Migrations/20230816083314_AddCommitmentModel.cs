using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddCommitmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCommitments",
                columns: table => new
                {
                    ProjectCommitmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectCommitmentName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    CommitmentNumber = table.Column<int>(type: "int", nullable: false),
                    Responsible = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    TaskStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlannedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommitments", x => x.ProjectCommitmentID);
                    table.ForeignKey(
                        name: "FK_ProjectCommitments_ProjectTaskStatus_TaskStatusID",
                        column: x => x.TaskStatusID,
                        principalTable: "ProjectTaskStatus",
                        principalColumn: "ProjectTaskStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCommitments_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommitments_ProjectID",
                table: "ProjectCommitments",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommitments_TaskStatusID",
                table: "ProjectCommitments",
                column: "TaskStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCommitments");
        }
    }
}

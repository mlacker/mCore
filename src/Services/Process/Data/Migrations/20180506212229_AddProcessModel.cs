using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mCore.Services.Process.Data.Migrations
{
    public partial class AddProcessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    TaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    UserTaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProcessInstanceId = table.Column<Guid>(nullable: false),
                    ActivityDefinitionId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    ClaimTime = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Category = table.Column<string>(maxLength: 200, nullable: false),
                    Version = table.Column<int>(nullable: false),
                    InitialActivityId = table.Column<Guid>(nullable: false),
                    IsSuspended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProcessDefinitionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityDefinition_ProcessDefinition_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProcessDefinitionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    BusinessKey = table.Column<string>(maxLength: 200, nullable: true),
                    StartUserId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessInstance_ProcessDefinition_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SourceId = table.Column<Guid>(nullable: false),
                    DestinationId = table.Column<Guid>(nullable: false),
                    TransitionType = table.Column<int>(nullable: false),
                    ProcessDefinitionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transition_ActivityDefinition_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "ActivityDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transition_ProcessDefinition_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transition_ActivityDefinition_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ActivityDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionDefinition_UserTaskId",
                table: "ActionDefinition",
                column: "UserTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDefinition_ProcessDefinitionId",
                table: "ActivityDefinition",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstance_ActivityDefinitionId",
                table: "ActivityInstance",
                column: "ActivityDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstance_ProcessInstanceId",
                table: "ActivityInstance",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ActionId",
                table: "Comment",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TaskId",
                table: "Comment",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDefinition_InitialActivityId",
                table: "ProcessDefinition",
                column: "InitialActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstance_ProcessDefinitionId",
                table: "ProcessInstance",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_DestinationId",
                table: "Transition",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_ProcessDefinitionId",
                table: "Transition",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_SourceId",
                table: "Transition",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ActionDefinition_ActionId",
                table: "Comment",
                column: "ActionId",
                principalTable: "ActionDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ActivityInstance_TaskId",
                table: "Comment",
                column: "TaskId",
                principalTable: "ActivityInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionDefinition_ActivityDefinition_UserTaskId",
                table: "ActionDefinition",
                column: "UserTaskId",
                principalTable: "ActivityDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInstance_ActivityDefinition_ActivityDefinitionId",
                table: "ActivityInstance",
                column: "ActivityDefinitionId",
                principalTable: "ActivityDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityInstance_ProcessInstance_ProcessInstanceId",
                table: "ActivityInstance",
                column: "ProcessInstanceId",
                principalTable: "ProcessInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDefinition_ActivityDefinition_InitialActivityId",
                table: "ProcessDefinition",
                column: "InitialActivityId",
                principalTable: "ActivityDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDefinition_ActivityDefinition_InitialActivityId",
                table: "ProcessDefinition");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Transition");

            migrationBuilder.DropTable(
                name: "ActionDefinition");

            migrationBuilder.DropTable(
                name: "ActivityInstance");

            migrationBuilder.DropTable(
                name: "ProcessInstance");

            migrationBuilder.DropTable(
                name: "ActivityDefinition");

            migrationBuilder.DropTable(
                name: "ProcessDefinition");
        }
    }
}

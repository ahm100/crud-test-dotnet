using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillsAndexperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Skill",
                table: "JobSeekerSkills",
                newName: "SkillName");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "JobSeekerExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndJobDate",
                table: "JobSeekerExperiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "JobSeekerExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartJobDate",
                table: "JobSeekerExperiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "JobPostings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_JobCategoryId",
                table: "JobPostings",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostings_JobCategory_JobCategoryId",
                table: "JobPostings",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_JobCategory_JobCategoryId",
                table: "JobPostings");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropIndex(
                name: "IX_JobPostings_JobCategoryId",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "JobSeekerExperiences");

            migrationBuilder.DropColumn(
                name: "EndJobDate",
                table: "JobSeekerExperiences");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "JobSeekerExperiences");

            migrationBuilder.DropColumn(
                name: "StartJobDate",
                table: "JobSeekerExperiences");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "JobPostings");

            migrationBuilder.RenameColumn(
                name: "SkillName",
                table: "JobSeekerSkills",
                newName: "Skill");
        }
    }
}

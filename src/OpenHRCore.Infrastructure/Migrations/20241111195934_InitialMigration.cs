using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenHRCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobGrades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    GradeName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MinSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    LevelName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    ParentOrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnits_OrganizationUnits_ParentOrganizationUnitId",
                        column: x => x.ParentOrganizationUnitId,
                        principalTable: "OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    JobTitle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPositions_OrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    JobLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobGradeId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobPositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_JobLevels_JobLevelId",
                        column: x => x.JobLevelId,
                        principalTable: "JobLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_OrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "JobGrades",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "GradeName", "IsActive", "IsDeleted", "MaxSalary", "MinSalary", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("91c7e3fc-6641-4c1f-8d45-9e6c99f25316"), "G1", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Entry level grade", "Grade 1", true, false, 45000m, 30000m, 1, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("92c7e3fc-6642-4c1f-8d45-9e6c99f25317"), "G2", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Mid level grade", "Grade 2", true, false, 65000m, 45001m, 2, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("93c7e3fc-6643-4c1f-8d45-9e6c99f25318"), "G3", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Senior level grade", "Grade 3", true, false, 90000m, 65001m, 3, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null }
                });

            migrationBuilder.InsertData(
                table: "JobLevels",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "LevelName", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81c7e3fc-664d-4c1f-8d45-9e6c99f25316"), "L1", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Entry level position", true, false, "Entry Level", 1, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("82c7e3fc-664e-4c1f-8d45-9e6c99f25317"), "L2", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Junior level position", true, false, "Junior Level", 2, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("83c7e3fc-664f-4c1f-8d45-9e6c99f25318"), "L3", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Senior level position", true, false, "Senior Level", 3, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null }
                });

            migrationBuilder.InsertData(
                table: "OrganizationUnits",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "Location", "Name", "ParentOrganizationUnitId", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("71c7e3fc-664a-4c1f-8d45-9e6c99f25316"), "HO", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Main headquarters", true, false, "New York", "Head Office", null, 1, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("72c7e3fc-664b-4c1f-8d45-9e6c99f25317"), "HR", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "HR Department", true, false, "New York", "Human Resources", new Guid("71c7e3fc-664a-4c1f-8d45-9e6c99f25316"), 2, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("73c7e3fc-664c-4c1f-8d45-9e6c99f25318"), "IT", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "IT Department", true, false, "New York", "Information Technology", new Guid("71c7e3fc-664a-4c1f-8d45-9e6c99f25316"), 3, new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null }
                });

            migrationBuilder.InsertData(
                table: "JobPositions",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "JobTitle", "OrganizationUnitId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a1c7e3fc-6644-4c1f-8d45-9e6c99f25316"), "HRM", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Human Resources Manager", true, false, "HR Manager", new Guid("72c7e3fc-664b-4c1f-8d45-9e6c99f25317"), new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("a2c7e3fc-6645-4c1f-8d45-9e6c99f25317"), "SWE", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, "Software Engineer", true, false, "Software Engineer", new Guid("73c7e3fc-664c-4c1f-8d45-9e6c99f25318"), new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Code", "CreatedAt", "CreatedBy", "DateOfBirth", "Email", "FirstName", "Gender", "IsActive", "IsDeleted", "JobGradeId", "JobLevelId", "JobPositionId", "LastName", "OrganizationUnitId", "Phone", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("b1c7e3fc-6646-4c1f-8d45-9e6c99f25316"), "123 Main St, New York", "EMP001", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, new DateOnly(1985, 5, 15), "john.doe@example.com", "John", "Male", true, false, new Guid("93c7e3fc-6643-4c1f-8d45-9e6c99f25318"), new Guid("83c7e3fc-664f-4c1f-8d45-9e6c99f25318"), new Guid("a1c7e3fc-6644-4c1f-8d45-9e6c99f25316"), "Doe", new Guid("72c7e3fc-664b-4c1f-8d45-9e6c99f25317"), "1234567890", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null },
                    { new Guid("b2c7e3fc-6647-4c1f-8d45-9e6c99f25317"), "456 Oak St, New York", "EMP002", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null, new DateOnly(1990, 8, 20), "jane.smith@example.com", "Jane", "Female", true, false, new Guid("92c7e3fc-6642-4c1f-8d45-9e6c99f25317"), new Guid("82c7e3fc-664e-4c1f-8d45-9e6c99f25317"), new Guid("a2c7e3fc-6645-4c1f-8d45-9e6c99f25317"), "Smith", new Guid("73c7e3fc-664c-4c1f-8d45-9e6c99f25318"), "0987654321", new DateTime(2024, 11, 11, 19, 59, 33, 752, DateTimeKind.Utc).AddTicks(769), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobGradeId",
                table: "Employees",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobLevelId",
                table: "Employees",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobPositionId",
                table: "Employees",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrganizationUnitId",
                table: "Employees",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_OrganizationUnitId",
                table: "JobPositions",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_ParentOrganizationUnitId",
                table: "OrganizationUnits",
                column: "ParentOrganizationUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "JobGrades");

            migrationBuilder.DropTable(
                name: "JobLevels");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "OrganizationUnits");
        }
    }
}

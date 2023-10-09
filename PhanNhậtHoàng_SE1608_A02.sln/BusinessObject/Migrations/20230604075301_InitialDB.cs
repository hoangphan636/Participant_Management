using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessObject.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyProjects",
                columns: table => new
                {
                    CompanyProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProjects", x => x.CompanyProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipatingProjects",
                columns: table => new
                {
                    CompanyProjectID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipatingProjects", x => new { x.CompanyProjectID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_ParticipatingProjects_CompanyProjects_CompanyProjectID",
                        column: x => x.CompanyProjectID,
                        principalTable: "CompanyProjects",
                        principalColumn: "CompanyProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipatingProjects_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CompanyProjects",
                columns: new[] { "CompanyProjectID", "EstimatedStartDate", "ExpectedEndDate", "ProjectDescription", "ProjectName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 4, 14, 53, 1, 287, DateTimeKind.Local).AddTicks(3942), new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(5779), "Project 1 Description", "Project 1" },
                    { 2, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6118), new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6122), "Project 2 Description", "Project 2" },
                    { 3, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6127), new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6128), "Project 3 Description", "Project 3" },
                    { 4, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6129), new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6130), "Project 4 Description", "Project 4" },
                    { 5, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6131), new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6132), "Project 5 Description", "Project 5" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentID", "DepartmentDescription", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Department 1 Description", "Department 1" },
                    { 2, "Department 2 Description", "Department 2" },
                    { 3, "Department 3 Description", "Department 3" },
                    { 4, "Department 4 Description", "Department 4" },
                    { 5, "Department 5 Description", "Department 5" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address", "DepartmentID", "EmailAddress", "FullName", "Password", "Skills", "Status", "Telephone" },
                values: new object[,]
                {
                    { 1, "Address 1", 1, "employee1@example.com", "Employee 1", "2", "Skill 1", "Active", "123456789" },
                    { 2, "Address 2", 2, "employee2@example.com", "Employee 2", "1", "Skill 2", "Active", "987654321" },
                    { 3, "Address 3", 3, "employee3@example.com", "Employee 3", "1", "Skill 3", "Active", "555555555" },
                    { 4, "Address 4", 4, "employee4@example.com", "Employee 4", "1", "Skill 4", "Active", "111111111" },
                    { 5, "Address 5", 5, "employee5@example.com", "Employee 5", "1", "Skill 5", "Active", "999999999" }
                });

            migrationBuilder.InsertData(
                table: "ParticipatingProjects",
                columns: new[] { "CompanyProjectID", "EmployeeID", "EndDate", "ProjectRole", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7157), 1, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(6989) },
                    { 1, 2, new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7490), 2, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7487) },
                    { 2, 3, new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7492), 1, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7492) },
                    { 2, 4, new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7495), 2, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7494) },
                    { 3, 5, new DateTime(2023, 7, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7497), 1, new DateTime(2023, 6, 4, 14, 53, 1, 288, DateTimeKind.Local).AddTicks(7496) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipatingProjects_EmployeeID",
                table: "ParticipatingProjects",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipatingProjects");

            migrationBuilder.DropTable(
                name: "CompanyProjects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

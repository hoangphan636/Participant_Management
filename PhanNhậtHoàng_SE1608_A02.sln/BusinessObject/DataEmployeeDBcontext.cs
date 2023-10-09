using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class DataEmployeeDBcontext: DbContext
    {
        public DataEmployeeDBcontext()
        {

        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }
        public Employee getDefaultAccounts()
        {
            var admin = new Employee();
            IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
         admin.EmailAddress = config["Credentials:Email"];
         admin.Password = config["Credentials:Password"];

            return admin;

        }
        public DbSet<Departmennt> Departments { get; set; }
        public DbSet<CompanyProject> CompanyProjects { get; set; }
        public DbSet<ParticipatingProject> ParticipatingProjects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // Other DbContext configurations if needed

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {




            optionsBuilder.UseSqlServer(GetConnectionString());

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ParticipatingProject>()
                 .HasKey(od => new { od.CompanyProjectID, od.EmployeeID });
            base.OnModelCreating(modelBuilder);

            // Custom configurations for your entities if needed

            modelBuilder.Entity<Departmennt>().HasData(
    new Departmennt
    {
        DepartmentID = 1,
        DepartmentName = "Department 1",
        DepartmentDescription = "Department 1 Description"
    },
    new Departmennt
    {
        DepartmentID = 2,
        DepartmentName = "Department 2",
        DepartmentDescription = "Department 2 Description"
    },
    new Departmennt
    {
        DepartmentID = 3,
        DepartmentName = "Department 3",
        DepartmentDescription = "Department 3 Description"
    },
    new Departmennt
    {
        DepartmentID = 4,
        DepartmentName = "Department 4",
        DepartmentDescription = "Department 4 Description"
    },
    new Departmennt
    {
        DepartmentID = 5,
        DepartmentName = "Department 5",
        DepartmentDescription = "Department 5 Description"
    }
);

            modelBuilder.Entity<CompanyProject>().HasData(
                new CompanyProject
                {
                    CompanyProjectID = 1,
                    ProjectName = "Project 1",
                    ProjectDescription = "Project 1 Description",
                    EstimatedStartDate = DateTime.Now,
                    ExpectedEndDate = DateTime.Now.AddDays(30)
                },
                new CompanyProject
                {
                    CompanyProjectID = 2,
                    ProjectName = "Project 2",
                    ProjectDescription = "Project 2 Description",
                    EstimatedStartDate = DateTime.Now,
                    ExpectedEndDate = DateTime.Now.AddDays(30)
                },
                new CompanyProject
                {
                    CompanyProjectID = 3,
                    ProjectName = "Project 3",
                    ProjectDescription = "Project 3 Description",
                    EstimatedStartDate = DateTime.Now,
                    ExpectedEndDate = DateTime.Now.AddDays(30)
                },
                new CompanyProject
                {
                    CompanyProjectID = 4,
                    ProjectName = "Project 4",
                    ProjectDescription = "Project 4 Description",
                    EstimatedStartDate = DateTime.Now,
                    ExpectedEndDate = DateTime.Now.AddDays(30)
                },
                new CompanyProject
                {
                    CompanyProjectID = 5,
                    ProjectName = "Project 5",
                    ProjectDescription = "Project 5 Description",
                    EstimatedStartDate = DateTime.Now,
                    ExpectedEndDate = DateTime.Now.AddDays(30)
                }
            );

            modelBuilder.Entity<ParticipatingProject>().HasData(
                new ParticipatingProject
                {
                    CompanyProjectID = 1,
                    EmployeeID = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    ProjectRole = 1 // Project Manager
                },
                new ParticipatingProject
                {
                    CompanyProjectID = 1,
                    EmployeeID = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    ProjectRole = 2 // Project Member
                },
                new ParticipatingProject
                {
                    CompanyProjectID = 2,
                    EmployeeID = 3,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    ProjectRole = 1 // Project Manager
                },
                new ParticipatingProject
                {
                    CompanyProjectID = 2,
                    EmployeeID = 4,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    ProjectRole = 2 // Project Member
                },
                new ParticipatingProject
                {
                    CompanyProjectID = 3,
                    EmployeeID = 5,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    ProjectRole = 1 // Project Manager
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeID = 1,
                    FullName = "Employee 1",
                    Skills = "Skill 1",
                    Telephone = "123456789",
                    Address = "Address 1",
                    Status = "Active",
                    DepartmentID = 1,
                    Password = "2",
                    EmailAddress = "employee1@example.com"
                },
                new Employee
                {
                    EmployeeID = 2,
                    FullName = "Employee 2",
                    Skills = "Skill 2",
                    Telephone = "987654321",
                    Address = "Address 2",
                    Status = "Active",
                    DepartmentID = 2,
                    Password = "1",
                    EmailAddress = "employee2@example.com"
                },
                new Employee
                {
                    EmployeeID = 3,
                    FullName = "Employee 3",
                    Skills = "Skill 3",
                    Telephone = "555555555",
                    Address = "Address 3",
                    Status = "Active",
                    DepartmentID = 3,
                    Password = "1",
                    EmailAddress = "employee3@example.com"
                },
                new Employee
                {
                    EmployeeID = 4,
                    FullName = "Employee 4",
                    Skills = "Skill 4",
                    Telephone = "111111111",
                    Address = "Address 4",
                    Status = "Active",
                    DepartmentID = 4,
                    Password = "1",
                    EmailAddress = "employee4@example.com"
                },
                new Employee
                {
                    EmployeeID = 5,
                    FullName = "Employee 5",
                    Skills = "Skill 5",
                    Telephone = "999999999",
                    Address = "Address 5",
                    Status = "Active",
                    DepartmentID = 5,
                    Password = "1",
                    EmailAddress = "employee5@example.com"
                }
            );

            base.OnModelCreating(modelBuilder);


        }


    }
}

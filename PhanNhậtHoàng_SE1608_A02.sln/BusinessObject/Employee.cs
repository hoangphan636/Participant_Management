using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Employee
    {
        public Employee()
        {
            ParticipatingProjects = new HashSet<ParticipatingProject>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Skills is required")]
        public string Skills { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "DepartmentID is required")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


        [Required(ErrorMessage = "ProjectName is required")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        // Define foreign key relationship
        [JsonIgnore]
        public virtual Departmennt Department { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParticipatingProject> ParticipatingProjects { get; set; }
    }
}

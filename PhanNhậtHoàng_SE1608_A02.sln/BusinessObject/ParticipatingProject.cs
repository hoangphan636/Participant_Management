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
    public class ParticipatingProject
    {
        [Required(ErrorMessage = "CompanyProjectID is required")]
        public int? CompanyProjectID { get; set; }

        [Required(ErrorMessage = "EmployeeID is required")]
        public int? EmployeeID { get; set; }

        [Required(ErrorMessage = "StartDate is required")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "ProjectRole is required")]
        [Range(1,2, ErrorMessage ="1 - 2")]
        public int? ProjectRole { get; set; }

      
        [JsonIgnore]
        public virtual CompanyProject CompanyProject { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }


    }
}

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
    public class CompanyProject
    {

        public CompanyProject()
        {
            ParticipatingProjects = new HashSet<ParticipatingProject>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CompanyProjectID { get; set; }

        [Required(ErrorMessage = "ProjectName is required")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "ProjectDescription is required")]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = "EstimatedStartDate is required")]
        public DateTime EstimatedStartDate { get; set; }

        [Required(ErrorMessage = "ExpectedEndDate is required")]
        public DateTime ExpectedEndDate { get; set; }
        [JsonIgnore]
        public virtual ICollection<ParticipatingProject> ParticipatingProjects { get; set; }

    }
}

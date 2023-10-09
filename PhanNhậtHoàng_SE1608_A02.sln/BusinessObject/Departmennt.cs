using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Departmennt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "DepartmentName is required")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "DepartmentDescription is required")]
        public string DepartmentDescription { get; set; }


    }
}

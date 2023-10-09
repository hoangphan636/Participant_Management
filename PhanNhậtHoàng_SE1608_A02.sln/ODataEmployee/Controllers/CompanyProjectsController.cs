using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Linq;
using Repository.Repositories;
using BusinessObject;
using System.Collections.Generic;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNetCore.OData.Formatter;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ODataEmployee.Controllers
{
    [EnableQuery]
    [Route("odata/CompanyProjects")]
    public class CompanyProjectsController : ODataController
    {
        CompanyProjectRepository com = new CompanyProjectRepository();

        [HttpGet]
        public ActionResult<IEnumerable<CompanyProject>> GetAll()
        {
            var companyProjects = com.GetCompanyProject();
            return Ok(companyProjects);
        }

        [HttpGet("search/{key}/{version}")]
        public IActionResult GetByIds(int key, string version)
        {
            var cus = com.SearchCompanyProject(version);

            return Ok(cus);
        }

        [HttpGet("{key}")]
        public IActionResult GetById(int key)
        {
            var cus = com.GetCompanyProjectID(key);
           
            return Ok(cus);
        }

  

        [HttpPut("updateProject")]
        public IActionResult Puta  ( [FromBody] CompanyProject companyProject)
        {
             com.UpdateCustomer(companyProject);
           return Ok();
        }




        [HttpPost("CreateProject")]
        public IActionResult Create([FromBody] CompanyProject companyProject)
        {
          
           
             com.SaveCustomer(companyProject);
            return RedirectToAction("GetById", new { key = companyProject.CompanyProjectID });
        }

        [HttpDelete("{key}")]
        public IActionResult Remove(int key)
        {
            var stu = com.GetCompanyProjectID(key);
            if (stu == null)
            {
                return NotFound();
            }

            com.DeleteCustomer(stu);

            return RedirectToAction("GetAll");
        }
    }

}

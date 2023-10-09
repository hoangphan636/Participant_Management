using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Repositories;
using System;
using System.Collections.Generic;

namespace ODataParticipatingProject.Controllers
{

    [EnableQuery]
    [Route("odata/ParticipatingProject")]
    public class ParticipatingProjectController : ODataController
    {
        ParticipatingProjectRepository com = new ParticipatingProjectRepository();

        [HttpGet]
        public ActionResult<IEnumerable<ParticipatingProject>> GetAll()
        {
            var ParticipatingProjects = com.GetParticipatingProject();
            return Ok(ParticipatingProjects);
        }



        [HttpGet("{key}")]
        public IActionResult GetById(int key)
        {
            var cus = com.GetParticipatingProjectID(key);

            return Ok(cus);
        }

        [HttpGet("search/{key}/{version}")]
        public IActionResult GetByIds(int key, string version)
        {
            var cus = com.CheckParticipatingProjectid(version);

            return Ok(cus);
        }

        [HttpPut("updateProject")]
        public IActionResult Puta([FromBody] ParticipatingProject ParticipatingProject)
        {
            com.UpdateCustomer(ParticipatingProject);
            return Ok();
        }




        [HttpPost("CreateProject")]
        public IActionResult Create([FromBody] ParticipatingProject ParticipatingProject)
        {
            var la = com.GetParticipatingProject();
            foreach (var item in la)
            {
                var CompanyProjectID = item.CompanyProjectID;
                var EmployeeID = item.EmployeeID;
                if(CompanyProjectID == ParticipatingProject.CompanyProjectID.Value && EmployeeID == ParticipatingProject.EmployeeID)
                {
                    string message = "CompanyProjectID,EmployeeID  has exist, please Update ";
                    return NotFound(message);
                }
               
            }
            com.SaveCustomer(ParticipatingProject);

            return Ok();
        }

        [HttpDelete("{key}")]
        public IActionResult Remove(int key)
        {
            var stu = com.GetParticipatingProjectID(key);
            if (stu == null)
            {
                return NotFound();
            }

            com.DeleteCustomer(stu);

            return RedirectToAction("GetAll");
        }






    }
}

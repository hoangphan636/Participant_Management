using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Repositories;
using System.Collections.Generic;

namespace ODataEmployee.Controllers
{

    [EnableQuery]
    [Route("odata/Departments")]
    public class DepartmentController : ODataController
    {

        DepartmentRepository com = new DepartmentRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Departmennt>> GetAll()
        {
            var Departmennts = com.GetDepartmennt();
            return Ok(Departmennts);
        }



        [HttpGet("{key}")]
        public IActionResult GetById(int key)
        {
            var cus = com.GetDepartmenntID(key);

            return Ok(cus);
        }



        [HttpPut("updateProject")]
        public IActionResult Puta([FromBody] Departmennt Departmennt)
        {
            com.UpdateCustomer(Departmennt);
            return Ok();
        }




        [HttpPost("CreateProject")]
        public IActionResult Create([FromBody] Departmennt Departmennt)
        {


            com.SaveCustomer(Departmennt);
            return RedirectToAction("GetById", new { key = Departmennt.DepartmentID });
        }

        [HttpDelete("{key}")]
        public IActionResult Remove(int key)
        {
            var stu = com.GetDepartmenntID(key);
            if (stu == null)
            {
                return NotFound();
            }

            com.DeleteCustomer(stu);

            return RedirectToAction("GetAll");
        }









    }
}

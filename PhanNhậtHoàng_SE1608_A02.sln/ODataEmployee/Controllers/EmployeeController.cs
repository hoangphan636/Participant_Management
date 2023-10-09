using BusinessObject;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Repositories;
using System.Collections.Generic;

namespace ODataEmployee.Controllers
{
    [EnableQuery]
    [Route("odata/Employee")]
    public class EmployeeController : ODataController
    {

        EmployeeRepository com = new EmployeeRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            var Employees = com.GetEmployee();
            return Ok(Employees);
        }

        [HttpGet("search/{key}/{version}")]
        public IActionResult GetByIds(int key, string version)
        {
            var cus = com.CheckEmployeeEmail(version);

            return Ok(cus);
        }

        [HttpGet("{key}")]
        public IActionResult GetById(int key)
        {
            var cus = com.GetEmployeeID(key);

            return Ok(cus);
        }



        [HttpPut("updateProject")]
        public IActionResult Puta([FromBody] Employee Employee)
        {
            if(com.CheckEmailForUpdate(Employee.EmailAddress,Employee.EmployeeID) ==  true)
            {
                com.UpdateCustomer(Employee);
                return Ok();
            }

            return NotFound("Email exist");

        }




        [HttpPost("CreateProject")]
        public IActionResult Create([FromBody] Employee Employee)
        {
           var mail =  com.CheckEmail(Employee.EmailAddress);
            if(mail == true)
            {
                
                com.SaveCustomer(Employee);
                return Ok();
            }

            return NotFound("Email exist");
        }

        [HttpDelete("{key}")]
        public IActionResult Remove(int key)
        {
            var stu = com.GetEmployeeID(key);
            if (stu == null)
            {
                return NotFound();
            }

            com.DeleteCustomer(stu);

            return RedirectToAction("GetAll");
        }



    }
}

using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Repositories;
using System.Reflection.Emit;

namespace ODataEmployee.Controllers
{
    [EnableQuery]
    [Route("odata/LoginEmployee")]
    public class LoginController : ODataController
    {
        EmployeeRepository com = new EmployeeRepository();
        [HttpPost("CreateProject")]
        public IActionResult Create([FromBody] Employee employee)
        {
            var admin = com.checkAdminLogin(employee.EmailAddress, employee.Password);
            var cus = com.checkCustomerLogin(employee.EmailAddress, employee.Password);
            if(admin != null)
            {
                return Ok("admin");
            }else if(cus != null)
            {
                return Ok("customer");
            }
            else {
                return NotFound("Not Founnd Email Or Password !! ");
            }   
          
        }
    }
}

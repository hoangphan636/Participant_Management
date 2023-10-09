using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace OdataManagementEmployee.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = null;
        private string productApiUrl = "";
        public LoginController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "http://localhost:43730/odata/LoginEmployee";
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string EmailAddress, string Password)
        {
            var ha = new Employee
            {
                EmployeeID = 0,
                FullName = null,
                Skills = null,
                Telephone = null,
                Address = null,
                Status = null,
                DepartmentID = 0,
                Password = Password,
                EmailAddress = EmailAddress
            };
            string strData = JsonSerializer.Serialize(ha);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{productApiUrl}/CreateProject", contentData);
                string strDatas = await response.Content.ReadAsStringAsync();
                JsonDocument document = JsonDocument.Parse(strDatas);
                JsonElement root = document.RootElement;
            if (response.IsSuccessStatusCode)
            {

                JsonElement roots = document.RootElement;

                string valueKindValue = roots.GetString();
                if (valueKindValue.Equals("customer"))
                {
                    HttpContext.Session.SetString("EmailAddress", ha.EmailAddress);
                }
                else if (valueKindValue.Equals("admin"))
                {
                    HttpContext.Session.SetString("EmailAddress", ha.EmailAddress);
                }


                return RedirectToAction("Index", "Employee");
            }
            else
            {
                JsonElement roots = document.RootElement;

                string valueKindValue = roots.GetString();


                TempData["learn"] = valueKindValue;
                return Redirect("/Login/Create");
            }
        }
    }
}

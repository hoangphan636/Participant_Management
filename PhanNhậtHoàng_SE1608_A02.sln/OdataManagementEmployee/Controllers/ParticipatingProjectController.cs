using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Repository;
using Microsoft.AspNetCore.Http;

namespace OdataManagementEmployee.Controllers
{
    public class ParticipatingProjectController : Controller
    {




        private readonly HttpClient client = null;
        private string productApiUrl = "";
        private string productApiUrls = "";
        private string productApiUrlss = "";

        public ParticipatingProjectController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "http://localhost:43730/odata/ParticipatingProject";
            productApiUrls = "http://localhost:43730/odata/CompanyProjects";
            productApiUrlss = "http://localhost:43730/odata/Employee";
        }
        public async Task<IActionResult> Index()
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null && !Email.Equals("admin@FURentalSystem.com"))
            {
                

                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/search/{1}/{Email}");
                string strData = await response.Content.ReadAsStringAsync();

                JArray jsonArray = JArray.Parse(strData);

                if (jsonArray != null)
                {
                    List<ParticipatingProject> items = jsonArray.Select(x => new ParticipatingProject
                    {
                        CompanyProjectID = (int)x["companyProjectID"],
                        EmployeeID = (int)x["employeeID"],
                        StartDate = (DateTime)x["startDate"],
                        EndDate = (DateTime)x["endDate"],
                        ProjectRole = (int)x["projectRole"],

                    }).ToList();

                    return View(items);
                }

                // Handle the case where jsonArray is null or empty
                // For example, return an empty list or display an appropriate message
                return View(new List<CompanyProject>());


            }
            else if (Email != null && Email.Equals("admin@FURentalSystem.com"))
            {
                HttpResponseMessage response = await client.GetAsync(productApiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(strData);
                List<ParticipatingProject> items = jsonArray.Select(x => new ParticipatingProject
                {
                    CompanyProjectID = (int)x["companyProjectID"],
                    EmployeeID = (int)x["employeeID"],
                    StartDate = (DateTime)x["startDate"],
                    EndDate = (DateTime)x["endDate"],
                    ProjectRole = (int)x["projectRole"],

                }).ToList();
                return View(items);

            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }

            return Redirect("/Login/Create");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Login/Create");
        }


        public async Task<IActionResult> Search(string searchQuery)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}?$filter=contains(tolower(ProjectName),'{searchQuery}') or contains(ProjectDescription,'{searchQuery}')");



                string strData = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(strData);
                List<ParticipatingProject> items = jsonArray.Select(x => new ParticipatingProject
                {
                    CompanyProjectID = (int)x["companyProjectID"],
                    EmployeeID = (int)x["employeeID"],
                    StartDate = (DateTime)x["startDate"],
                    EndDate = (DateTime)x["endDate"],
                    ProjectRole = (int)x["projectRole"],

                }).ToList();

                return View("Index", items);
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }

        public ActionResult CompanyProjects()
        {
            return RedirectToAction("Index", "CompanyProject");

        }
        public ActionResult Employees()
        {
            return RedirectToAction("Index", "Employee");

        }

        public async Task<IActionResult> Created()
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync(productApiUrls);
                HttpResponseMessage responses = await client.GetAsync(productApiUrlss);

                string strData = response.Content.ReadAsStringAsync().Result;
                string strDatas = responses.Content.ReadAsStringAsync().Result;

                JsonDocument document = JsonDocument.Parse(strData);
                JsonDocument documents = JsonDocument.Parse(strDatas);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                JsonElement root = document.RootElement;
                JsonElement roots = documents.RootElement;

                JsonElement supplierElement = root.GetProperty("value");
                //    JsonElement categoryElement = roots.GetProperty("value");

                List<CompanyProject> companyProjects = JsonSerializer.Deserialize<List<CompanyProject>>(supplierElement.GetRawText(), options);
                List<Employee> employee = JsonSerializer.Deserialize<List<Employee>>(roots.GetRawText(), options);

                ViewBag.CompanyProjectID = new SelectList(companyProjects, nameof(CompanyProject.CompanyProjectID), nameof(CompanyProject.ProjectName));
                ViewBag.EmployeeID = new SelectList(employee, nameof(Employee.EmployeeID), nameof(Employee.FullName));
                return View("Create");
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creation(ParticipatingProject p)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                if (ModelState.IsValid)
                {

                    string strData = JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{productApiUrl}/CreateProject", contentData);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string strDatas = response.Content.ReadAsStringAsync().Result;
                        JsonDocument document = JsonDocument.Parse(strDatas);
                        JsonElement roots = document.RootElement;

                        string valueKindValue = roots.GetString();


                        ViewBag.learn = valueKindValue;
                        return View("Create");
                    }
                }
                else if (Email == null)
                {
                    return Redirect("/Login/Create");
                }
                return Redirect("/Login/Create");

            }

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Details(int id)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/{id}");
                string strData = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(strData))
                {
                    JsonElement root = document.RootElement;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };


                    ParticipatingProject product = JsonSerializer.Deserialize<ParticipatingProject>(root.GetRawText(), options);

                    return View(product);
                }
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/{id}");
                string strData = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(strData))
                {
                    JsonElement root = document.RootElement;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };


                    ParticipatingProject product = JsonSerializer.Deserialize<ParticipatingProject>(root.GetRawText(), options);

                    return View(product);
                    //}
                }
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }


        public async Task<IActionResult> Update(ParticipatingProject product)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{productApiUrl}/updateProject", content);

                return RedirectToAction(nameof(Index));
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }


        public async Task<IActionResult> Deleted(int id)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.DeleteAsync($"{productApiUrl}/{id}");
                string strData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");


        }

        public async Task<IActionResult> Delete(int id)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/{id}");
                string strData = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(strData))
                {
                    JsonElement root = document.RootElement;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Lấy đối tượng "product" từ JSON

                    ParticipatingProject product = JsonSerializer.Deserialize<ParticipatingProject>(root.GetRawText(), options);

                    return View(product);
                }
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");

        }
    }
}

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
using Microsoft.AspNetCore.Http;

namespace OdataManagementEmployee.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly HttpClient client = null;
        private string productApiUrl = "";
        private string productApiUrls = "";
        public EmployeeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "http://localhost:43730/odata/Employee";
            productApiUrls = "http://localhost:43730/odata/Departments";
        }
        public async Task<IActionResult> Index()
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null && !Email.Equals("admin@FURentalSystem.com"))
            {
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/search/{1}/{Email}");


                string strData = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(strData);
                List<Employee> items = jsonArray.Select(x => new Employee
                {
                    EmployeeID = (int)x["employeeID"],
                    FullName = (string)x["fullName"],
                    Skills = (string)x["skills"],
                    Telephone = (string)x["telephone"],
                    Address = (string)x["address"],
                    Status = (string)x["status"],
                    DepartmentID = (int)x["departmentID"],
                    Password = (string)x["password"],
                    EmailAddress = (string)x["emailAddress"]
                }).ToList();

                return View(items);
            }
            else if (Email != null && Email.Equals("admin@FURentalSystem.com"))
            {
                HttpResponseMessage response = await client.GetAsync(productApiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(strData);
                List<Employee> items = jsonArray.Select(x => new Employee
                {
                    EmployeeID = (int)x["employeeID"],
                    FullName = (string)x["fullName"],
                    Skills = (string)x["skills"],
                    Telephone = (string)x["telephone"],
                    Address = (string)x["address"],
                    Status = (string)x["status"],
                    DepartmentID = (int)x["departmentID"],
                    Password = (string)x["password"],
                    EmailAddress = (string)x["emailAddress"]
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

                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}?$filter=contains((FullName),'{searchQuery}')");
                string strData = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(strData);
                List<Employee> items = jsonArray.Select(x => new Employee
                {
                    EmployeeID = (int)x["employeeID"],
                    FullName = (string)x["fullName"],
                    Skills = (string)x["skills"],
                    Telephone = (string)x["telephone"],
                    Address = (string)x["address"],
                    Status = (string)x["status"],
                    DepartmentID = (int)x["departmentID"],
                    Password = (string)x["password"],
                    EmailAddress = (string)x["emailAddress"]
                }).ToList();

                return View("Index", items);
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }

        public async Task<IActionResult> Created()
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync(productApiUrls);


                string strData = response.Content.ReadAsStringAsync().Result;


                JsonDocument document = JsonDocument.Parse(strData);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                JsonElement root = document.RootElement;


                JsonElement supplierElement = root.GetProperty("value");
                //    JsonElement categoryElement = roots.GetProperty("value");

                List<Departmennt> companyProjects = JsonSerializer.Deserialize<List<Departmennt>>(supplierElement.GetRawText(), options);


                ViewBag.DepartmentID = new SelectList(companyProjects, nameof(Departmennt.DepartmentID), nameof(Departmennt.DepartmentName));

                return View("Create");
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");

        }
        public ActionResult CompanyProject()
        {
            return RedirectToAction("Index", "CompanyProject");

        }
        public ActionResult Participating()
        {
            return RedirectToAction("Index", "ParticipatingProject");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creation(Employee p)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
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
                    HttpResponseMessage responses = await client.GetAsync(productApiUrls);


                    string strDatass = responses.Content.ReadAsStringAsync().Result;


                    JsonDocument documents = JsonDocument.Parse(strDatass);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    JsonElement root = documents.RootElement;


                    JsonElement supplierElement = root.GetProperty("value");


                    List<Departmennt> companyProjects = JsonSerializer.Deserialize<List<Departmennt>>(supplierElement.GetRawText(), options);


                    ViewBag.DepartmentID = new SelectList(companyProjects, nameof(Departmennt.DepartmentID), nameof(Departmennt.DepartmentName));

                    return View("Create");
                }
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");



            //  return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Details(int id)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/{id}");
                string strData = await response.Content.ReadAsStringAsync();



                JsonDocument document = JsonDocument.Parse(strData);



                JsonElement root = document.RootElement;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                Employee product = JsonSerializer.Deserialize<Employee>(root.GetRawText(), options);

                return View(product);
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");

        }

        public async Task<IActionResult> Edit(int id, string email)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                HttpResponseMessage responses = await client.GetAsync(productApiUrls);
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/{id}");
                string strData = await response.Content.ReadAsStringAsync();
                string strDatas = responses.Content.ReadAsStringAsync().Result;
                JsonDocument documents = JsonDocument.Parse(strDatas);
                JsonDocument document = JsonDocument.Parse(strData);

                JsonElement roots = documents.RootElement;
                JsonElement root = document.RootElement;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                JsonElement supplierElement = roots.GetProperty("value");
                List<Departmennt> companyProjects = JsonSerializer.Deserialize<List<Departmennt>>(supplierElement.GetRawText(), options);


                ViewBag.DepartmentID = new SelectList(companyProjects, nameof(Departmennt.DepartmentID), nameof(Departmennt.DepartmentName));

                Employee product = JsonSerializer.Deserialize<Employee>(root.GetRawText(), options);

                return View(product);
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");


        }


        public async Task<IActionResult> Update(Employee product)
        {
            string Email = HttpContext.Session.GetString("EmailAddress");


            if (Email != null)
            {
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{productApiUrl}/updateProject", content);


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
                    HttpResponseMessage responses = await client.GetAsync(productApiUrls);


                    string strDatass = responses.Content.ReadAsStringAsync().Result;


                    JsonDocument documents = JsonDocument.Parse(strDatass);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    JsonElement root = documents.RootElement;


                    JsonElement supplierElement = root.GetProperty("value");
                    //    JsonElement categoryElement = roots.GetProperty("value");

                    List<Departmennt> companyProjects = JsonSerializer.Deserialize<List<Departmennt>>(supplierElement.GetRawText(), options);


                    ViewBag.DepartmentID = new SelectList(companyProjects, nameof(Departmennt.DepartmentID), nameof(Departmennt.DepartmentName));

                    return View("Edit");

                }
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
                return RedirectToAction(nameof(Index));
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

                    Employee product = JsonSerializer.Deserialize<Employee>(root.GetRawText(), options);

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

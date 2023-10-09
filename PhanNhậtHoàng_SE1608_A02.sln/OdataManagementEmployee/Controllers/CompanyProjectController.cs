using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using BusinessObject;
using System.Text;

namespace OdataManagementEmployee.Controllers
{
    public class CompanyProjectController : Controller
    {

        private readonly HttpClient client = null;
        private string productApiUrl = "";
        public CompanyProjectController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "http://localhost:43730/odata/CompanyProjects";
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
                    List<CompanyProject> items = jsonArray.Select(x => new CompanyProject
                    {
                        CompanyProjectID = (int)x["CompanyProjectID"],
                        ProjectName = (string)x["ProjectName"],
                        ProjectDescription = (string)x["ProjectDescription"],
                        EstimatedStartDate = (DateTime)x["EstimatedStartDate"],
                        ExpectedEndDate = (DateTime)x["ExpectedEndDate"]
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
                dynamic temp = JObject.Parse(strData);
                var lst = temp.value;
                List<CompanyProject> items = ((JArray)temp.value).Select(x => new CompanyProject
                {
                    CompanyProjectID = (int)x["CompanyProjectID"],
                    ProjectName = (string)x["ProjectName"],
                    ProjectDescription = (string)x["ProjectDescription"],
                    EstimatedStartDate = (DateTime)x["EstimatedStartDate"],
                    ExpectedEndDate = (DateTime)x["ExpectedEndDate"]

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
                HttpResponseMessage response = await client.GetAsync($"{productApiUrl}?$filter=contains((ProjectName),'{searchQuery}') or contains(ProjectDescription,'{searchQuery}')");
                string strData = await response.Content.ReadAsStringAsync();

                JArray jsonArray = JArray.Parse(strData);

                if (jsonArray != null)
                {
                    List<CompanyProject> items = jsonArray.Select(x => new CompanyProject
                    {
                        CompanyProjectID = (int)x["CompanyProjectID"],
                        ProjectName = (string)x["ProjectName"],
                        ProjectDescription = (string)x["ProjectDescription"],
                        EstimatedStartDate = (DateTime)x["EstimatedStartDate"],
                        ExpectedEndDate = (DateTime)x["ExpectedEndDate"]
                    }).ToList();

                    return View(items);
                }
            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
        }

        public ActionResult Create()
        {
            return View();

        }

        public ActionResult Employee()
        {
            return RedirectToAction("Index", "Employee");

        }
        public ActionResult Participating()
        {
            return RedirectToAction("Index", "ParticipatingProject");

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyProject p)
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
                        ViewBag.Message = "Insert successfully!";
                    }
                    else
                    {
                        ViewBag.Message = "Error while calling WebAPI!";
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");
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


                    CompanyProject product = JsonSerializer.Deserialize<CompanyProject>(root.GetRawText(), options);

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

                JsonDocument document = JsonDocument.Parse(strData);
                JsonElement root = document.RootElement;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                CompanyProject product = JsonSerializer.Deserialize<CompanyProject>(root.GetRawText(), options);

                return View(product);

            }
            else if (Email == null)
            {
                return Redirect("/Login/Create");
            }
            return Redirect("/Login/Create");

        }


        public async Task<IActionResult> Update(CompanyProject product)
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

                    CompanyProject product = JsonSerializer.Deserialize<CompanyProject>(root.GetRawText(), options);

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

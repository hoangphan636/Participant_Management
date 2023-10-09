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

namespace OdataManagementEmployee.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly HttpClient client = null;
        private string productApiUrl = "";
        public DepartmentController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "http://localhost:43730/odata/Departments";
        }
        public async Task<IActionResult> Index()
        {

            HttpResponseMessage response = await client.GetAsync(productApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Departmennt> items = ((JArray)temp.value).Select(x => new Departmennt
            {
                DepartmentID = (int)x["DepartmenntID"],
                DepartmentName = (string)x["ProjectName"],
                DepartmentDescription = (string)x["ProjectDescription"],
              
            }).ToList();
            return View(items);
        }


        public async Task<IActionResult> Search(string searchQuery)
        {

            HttpResponseMessage response = await client.GetAsync($"{productApiUrl}?$filter=contains(tolower(ProjectName),'{searchQuery}') or contains(ProjectDescription,'{searchQuery}')");



            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Departmennt> items = ((JArray)temp.value).Select(x => new Departmennt
            {
                DepartmentID = (int)x["DepartmenntID"],
                DepartmentName = (string)x["ProjectName"],
                DepartmentDescription = (string)x["ProjectDescription"],

            }).ToList();

            return View("Index", items);
        }

        public ActionResult Create()
        {

            return View();

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departmennt p)
        {
            //string Email = HttpContext.Session.GetString("Email");


            //if (Email != null)
            //{
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

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Details(int id)
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


                Departmennt product = JsonSerializer.Deserialize<Departmennt>(root.GetRawText(), options);

                return View(product);
            }
            //}
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Edit(int id)
        {
            //string Email = HttpContext.Session.GetString("Email");
            //if (Email != null)
            //{
            HttpResponseMessage response = await client.GetAsync($"{productApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            using (JsonDocument document = JsonDocument.Parse(strData))
            {
                JsonElement root = document.RootElement;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                Departmennt product = JsonSerializer.Deserialize<Departmennt>(root.GetRawText(), options);

                return View(product);
                //}
            }
            return RedirectToAction("Index", "Login");
        }


        public async Task<IActionResult> Update(Departmennt product)
        {

            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{productApiUrl}/updateProject", content);

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Deleted(int id)
        {

            HttpResponseMessage response = await client.DeleteAsync($"{productApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Delete(int id)
        {
            //string Email = HttpContext.Session.GetString("Email");
            //if (Email != null)
            //{
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

                Departmennt product = JsonSerializer.Deserialize<Departmennt>(root.GetRawText(), options);

                return View(product);
            }
            //}
            return RedirectToAction("Index", "Login");

        }












    }
}

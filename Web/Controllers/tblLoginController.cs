using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class tblLoginController : Controller
    {


        public ActionResult create()
        {
            ViewBag.showSuccessAlert = false;
            return View();
        }

        [HttpPost]
        public ActionResult create(tblLoginViewModel tblLogin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblLogin");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<tblLoginViewModel>("tblLogin", tblLogin);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create");
                    ViewBag.showSuccessAlert = true;
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(tblLogin);

        }

        private HttpClient CreateAuthenticatedSession()
        {
            var Client = new HttpClient { BaseAddress = new Uri("http://localhost:4701/api/") };
            var Result = Client.PostAsync("/Account/Logon/",
                             new FormUrlEncodedContent(
                                 new Dictionary<string, string>{ {"UserName", "Username"},
                                 {
                                     "Password","Password"
                                 }})).Result;
            Result.EnsureSuccessStatusCode();
            return Client;
        }

        public void GenerateByDay(int days, string path)
        {
            var HttpClientSession = CreateAuthenticatedSession();
            HttpClientSession.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var Result = HttpClientSession.GetAsync("http://localhost:4701/api/sync/?days=" + days.ToString(CultureInfo.InvariantCulture)).Result;
            Result.EnsureSuccessStatusCode();
            var ResponseBody = Result.Content.ReadAsStringAsync().Result;
            //deserialize result and continue on 
        }
    }
}
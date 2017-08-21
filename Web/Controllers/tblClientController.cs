using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class tblClientController : Controller
    {
        // GET: tblClient
        public ActionResult Index()
        {
            IEnumerable<tblClientViewModel> clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblClient");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<tblClientViewModel>>();
                    readTask.Wait();

                    clientes = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    clientes = Enumerable.Empty<tblClientViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(clientes);
        }

        public ActionResult create()
        {
            return View();
        }
    }
}
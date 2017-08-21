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

        [HttpPost]
        public ActionResult create(tblClientViewModel tblClient)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblClient");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<tblClientViewModel>("tblClient", tblClient);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(tblClient);
        }

        public ActionResult Edit(int id)
        {
            tblClientViewModel clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblClient?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<tblClientViewModel>();
                    readTask.Wait();

                    clientes = readTask.Result;
                }
            }

            return View(clientes);
        }

        [HttpPost]
        public ActionResult Edit(tblClientViewModel tblClient)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblClient");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<tblClientViewModel>("tblClient", tblClient);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(tblClient);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("tblClient/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
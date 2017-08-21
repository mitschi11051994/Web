using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class tblContactController : Controller
    {
        // GET: tblClient
        public ActionResult Index()
        {
            IEnumerable<tblContactViewModel> contactos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblContact");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<tblContactViewModel>>();
                    readTask.Wait();

                    contactos = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    contactos = Enumerable.Empty<tblContactViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(contactos);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(tblContactViewModel tblContact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblContact");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<tblContactViewModel>("tblContact", tblContact);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(tblContact);
        }

        public ActionResult Edit(int id)
        {
            tblContactViewModel contactos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblContact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<tblContactViewModel>();
                    readTask.Wait();

                    contactos = readTask.Result;
                }
            }

            return View(contactos);
        }

        [HttpPost]
        public ActionResult Edit(tblContactViewModel tblContact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblContact");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<tblContactViewModel>("tblContact", tblContact);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(tblContact);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("tblContact/" + id.ToString());
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
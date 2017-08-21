using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class tblReunionController : Controller
    {
        // GET: tblReunon
        public ActionResult Index()
        {
            IEnumerable<tblReunionViewModel> reunion = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblReunion");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<tblReunionViewModel>>();
                    readTask.Wait();

                    reunion = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    reunion = Enumerable.Empty<tblReunionViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(reunion);
        }

        public ActionResult create()
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            return View();
        }

        [HttpPost]
        public ActionResult create(tblReunionViewModel tblReunion)
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblReunion");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<tblReunionViewModel>("tblReunion", tblReunion);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(tblReunion);
        }

        public ActionResult Edit(int id)
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            tblReunionViewModel reunion = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblReunion?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<tblReunionViewModel>();
                    readTask.Wait();

                    reunion = readTask.Result;
                }
            }

            return View(reunion);
        }

        [HttpPost]
        public ActionResult Edit(tblReunionViewModel tblReunion)
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblReunion");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<tblReunionViewModel>("tblReunion", tblReunion);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(tblReunion);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("tblReunion/" + id.ToString());
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class tblSupport_TicketsController : Controller
    {
        // GET: tblSupport_Tickets
        public ActionResult Index()
        {
            IEnumerable<tblSupport_TicketsViewModel> tiquetes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblSupport_Tickets");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<tblSupport_TicketsViewModel>>();
                    readTask.Wait();

                    tiquetes = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    tiquetes = Enumerable.Empty<tblSupport_TicketsViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(tiquetes);
        }

        public ActionResult create()
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            return View();
        }

        [HttpPost]
        public ActionResult create(tblSupport_TicketsViewModel tblSupport_Tickets)
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblSupport_Tickets");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<tblSupport_TicketsViewModel>("tblSupport_Tickets", tblSupport_Tickets);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(tblSupport_Tickets);
        }
        public ActionResult Edit(int id)
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            tblSupport_TicketsViewModel tiquete = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tblSupport_Tickets?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<tblSupport_TicketsViewModel>();
                    readTask.Wait();

                    tiquete = readTask.Result;
                }
            }

            return View(tiquete);
        }

        [HttpPost]
        public ActionResult Edit(tblSupport_TicketsViewModel tblReunion)
        {
            CMDEntities db = new CMDEntities();
            ViewBag.TipoCliente = new SelectList(db.tblClient, "id_client", "name");
            ViewBag.TipoUsuario = new SelectList(db.tblLogin, "id_user", "username");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4701/api/tblSupport_Tickets");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<tblSupport_TicketsViewModel>("tblSupport_Tickets", tblReunion);
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
                var deleteTask = client.DeleteAsync("tblSupport_Tickets/" + id.ToString());
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
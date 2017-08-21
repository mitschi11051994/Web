using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers.API
{
    public class tblClientController : ApiController
    {
        public IHttpActionResult GetAlltblClient(bool includeAddress = false)
        {
            IList<tblClientViewModel> clientes = null;
            using (var ctx = new CMDEntities())
            {
                clientes = ctx.tblClient.Select(s => new tblClientViewModel()
                {
                    id_client = s.id_client,
                    name = s.name,
                    web_page = s.web_page,
                    direccion = s.direccion,
                    tel = s.tel,
                    puesto = s.puesto
                }).ToList<tblClientViewModel>();
            }
            if (clientes.Count == 0)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        public IHttpActionResult GettblClientById(int id)
        {
            tblClientViewModel clientes = null;

            using (var ctx = new CMDEntities())
            {
                clientes = ctx.tblClient.Where(s => s.id_client == id)
                    .Select(s => new tblClientViewModel()
                    {
                        id_client = s.id_client,
                        name = s.name,
                        web_page = s.web_page,
                        direccion = s.direccion,
                        tel = s.tel,
                        puesto = s.puesto
                    }).FirstOrDefault<tblClientViewModel>();
            }

            if (clientes == null)
            {
                return NotFound();
            }

            return Ok(clientes);
        }

        public IHttpActionResult GetAlltblClient(string name)
        {
            IList<tblClientViewModel> clientes = null;

            using (var ctx = new CMDEntities())
            {
                clientes = ctx.tblClient.Where(s => s.name.ToLower() == name.ToLower())
                    .Select(s => new tblClientViewModel()
                    {
                        id_client = s.id_client,
                        name = s.name,
                        web_page = s.web_page,
                        direccion = s.direccion,
                        tel = s.tel,
                        puesto = s.puesto
                    }).ToList<tblClientViewModel>();
            }
            if (clientes.Count == 0)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        //Get action methods of the previous section

        public IHttpActionResult PostNewtblClient(tblClientViewModel cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new CMDEntities())
            {
                ctx.tblClient.Add(new tblClient()
                {
                    id_client = cliente.id_client,
                    name = cliente.name,
                    web_page = cliente.web_page,
                    direccion = cliente.direccion,
                    tel = cliente.tel,
                    puesto = cliente.puesto
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(tblClientViewModel tblClient)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CMDEntities())
            {
                var existingtblClient = ctx.tblClient.Where(s => s.id_client == tblClient.id_client)
                                                        .FirstOrDefault<tblClient>();

                if (existingtblClient != null)
                {
                    existingtblClient.id_client = tblClient.id_client;
                    existingtblClient.name = tblClient.name;
                    existingtblClient.web_page = tblClient.web_page;
                    existingtblClient.direccion = tblClient.direccion;
                    existingtblClient.tel = tblClient.tel;
                    existingtblClient.puesto = tblClient.puesto;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult DeletetblClient(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new CMDEntities())
            {
                var cliente = ctx.tblClient
                    .Where(s => s.id_client == id)
                    .FirstOrDefault();

                ctx.Entry(cliente).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }



    }
}

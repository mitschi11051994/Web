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

        public IHttpActionResult GetAllStblClient(string name)
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




    }
}

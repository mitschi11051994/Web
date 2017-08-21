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
        public IHttpActionResult GetAlltblClient()
        {
            using (var ctx = new CMDEntities())
            {
                clientes = ctx.tblClient.Select(s => new tblClientViewModel()
                {
                    id_client = s.id_client,
                    name = s.name,
                    web_page = s.web_page,
                    direccion = s.direccion,
                    tel = s.tel,
                    puesto= s.puesto}).ToList<tblClientViewModel>();
            }

            if (clientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(clientes);
        }
    }
}

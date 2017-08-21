using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers.API
{
    public class tblSupport_TiketController : ApiController
    {
        public IHttpActionResult GetAlltSupport_Tiket(bool includeAddress = false)
        {
            IList<tblSupport_TiketViewModel> tiquetes = null;
            using (var ctx = new CMDEntities())
            {
                tiquetes = ctx.tblSupport_Tickets.Select(s => new tblSupport_TiketViewModel()
                {
                    id_Support_Tickets= s.id_Support_Tickets,
                    title = s.title,
                    detalle = s.detalle,
                    id_user = s.id_user,                    
                    id_client= s.id_client,
                    estado = s.estado,
                }).ToList<tblSupport_TiketViewModel>();
            }
            if (tiquetes.Count == 0)
            {
                return NotFound();
            }
            return Ok(tiquetes);
        }

        public IHttpActionResult GettblSupport_TiketById(int id)
        {
            tblSupport_TiketViewModel tiquetes = null;

            using (var ctx = new CMDEntities())
            {
                tiquetes = ctx.tblSupport_Tickets.Where(s => s.id_Support_Tickets == id)
                    .Select(s => new tblSupport_TiketViewModel()
                    {
                        id_Support_Tickets = s.id_Support_Tickets,
                        title = s.title,
                        detalle = s.detalle,
                        id_user = s.id_user,
                        id_client = s.id_client,
                        estado = s.estado,
                    }).FirstOrDefault<tblSupport_TiketViewModel>();
            }

            if (tiquetes == null)
            {
                return NotFound();
            }

            return Ok(tiquetes);
        }

        public IHttpActionResult GetAlltblSupport_Tiket(string title)
        {
            IList<tblSupport_TiketViewModel> tiquete = null;

            using (var ctx = new CMDEntities())
            {
                tiquete = ctx.tblSupport_Tickets.Where(s => s.title.ToLower() == title.ToLower())
                    .Select(s => new tblSupport_TiketViewModel()
                    {
                        id_Support_Tickets = s.id_Support_Tickets,
                        title = s.title,
                        detalle = s.detalle,
                        id_user = s.id_user,
                        id_client = s.id_client,
                        estado = s.estado,
                    }).ToList<tblSupport_TiketViewModel>();
            }
            if (tiquete.Count == 0)
            {
                return NotFound();
            }
            return Ok(tiquete);
        }

        //Get action methods of the previous section

        public IHttpActionResult PostNewtblSupport_Tiket(tblSupport_TiketViewModel tiquetes)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new CMDEntities())
            {
                ctx.tblSupport_Tickets.Add(new tblSupport_Tickets()
                {
                    id_Support_Tickets = tiquetes.id_Support_Tickets,
                    title = tiquetes.title,
                    detalle = (tiquetes.detalle),
                    id_user = tiquetes.id_user,
                     id_client = tiquetes.id_client,
                    estado = tiquetes.estado,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(tblSupport_TiketViewModel tblSupport_Tickets)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CMDEntities())
            {
                var existingtbReunion = ctx.tblSupport_Tickets.Where(s => s.id_Support_Tickets == tblSupport_Tickets.id_Support_Tickets)
                                                        .FirstOrDefault<tblSupport_Tickets>();

                if (existingtbReunion != null)
                {
                    existingtbReunion.id_Support_Tickets = tblSupport_Tickets.id_Support_Tickets;
                    existingtbReunion.title = tblSupport_Tickets.title;
                    existingtbReunion.detalle = tblSupport_Tickets.detalle;
                    existingtbReunion.id_user = tblSupport_Tickets.id_user;
                    existingtbReunion.id_client = tblSupport_Tickets.id_client;
                    existingtbReunion.estado = tblSupport_Tickets.estado;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new CMDEntities())
            {
                var tiquetes = ctx.tblSupport_Tickets
                    .Where(s => s.id_Support_Tickets == id)
                    .FirstOrDefault();

                ctx.Entry(tiquetes).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }



    }
}

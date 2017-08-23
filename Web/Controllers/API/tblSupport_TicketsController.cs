using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Web.Models;

namespace Web.Controllers.API
{
    public class tblSupport_TicketsController : ApiController
    {
        public IHttpActionResult GetAlltblSupport_Tickets(bool includeAddress = false)
        {
            IList<tblSupport_TicketsViewModel> tiquetes = null;
            using (var ctx = new CMDEntities())
            {
                tiquetes = ctx.tblSupport_Tickets.Select(s => new tblSupport_TicketsViewModel()
                {
                    id_Support_Tickets = s.id_Support_Tickets,
                    title = s.title,
                    detalle = s.detalle,
                    id_user = s.id_user.Value,
                    id_client = s.id_client.Value,
                    estado = s.estado
                }).ToList<tblSupport_TicketsViewModel>();
            }
            if (tiquetes.Count == 0)
            {
                return NotFound();
            }
            return Ok(tiquetes);
        }

        public IHttpActionResult GettblSupport_Tickets(int id)
        {
            tblSupport_TicketsViewModel tiquetes = null;

            using (var ctx = new CMDEntities())
            {
                tiquetes = ctx.tblSupport_Tickets.Where(s => s.id_Support_Tickets == id)
                    .Select(s => new tblSupport_TicketsViewModel()
                    {
                        id_Support_Tickets = s.id_Support_Tickets,
                        title = s.title,
                        detalle = s.detalle,
                        id_user = s.id_user.Value,
                        id_client = s.id_client.Value,
                        estado = s.estado
                    }).FirstOrDefault<tblSupport_TicketsViewModel>();
            }

            if (tiquetes == null)
            {
                return NotFound();
            }

            return Ok(tiquetes);
        }

        public IHttpActionResult GetAllSupport_Tickets(string title)
        {
            IList<tblSupport_TicketsViewModel> tiquetes = null;

            using (var ctx = new CMDEntities())
            {
                tiquetes = ctx.tblSupport_Tickets.Where(s => s.title.ToLower() == title.ToLower())
                    .Select(s => new tblSupport_TicketsViewModel()
                    {
                        id_Support_Tickets = s.id_Support_Tickets,
                        title = s.title,
                        detalle = s.detalle,
                        id_user = s.id_user.Value,
                        id_client = s.id_client.Value,
                        estado = s.estado
                    }).ToList<tblSupport_TicketsViewModel>();
            }
            if (tiquetes.Count == 0)
            {
                return NotFound();
            }
            return Ok(tiquetes);
        }

        //Get action methods of the previous section

        public IHttpActionResult PostNewtblSupport_Tickets(tblSupport_TicketsViewModel tblSupport_Tickets)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new CMDEntities())
            {
                ctx.tblSupport_Tickets.Add(new tblSupport_Tickets()
                {
                    id_Support_Tickets=tblSupport_Tickets.id_Support_Tickets,
                    title = tblSupport_Tickets.title,
                    detalle = tblSupport_Tickets.detalle,
                    id_user = tblSupport_Tickets.id_user,
                    id_client = tblSupport_Tickets.id_client,
                    estado = tblSupport_Tickets.estado
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(tblSupport_TicketsViewModel tiquetes)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CMDEntities())
            {
                var existingtbReunion = ctx.tblSupport_Tickets.Where(s => s.id_Support_Tickets == tiquetes.id_Support_Tickets)
                                                        .FirstOrDefault<tblSupport_Tickets>();

                if (existingtbReunion != null)
                {
                    existingtbReunion.id_Support_Tickets = tiquetes.id_Support_Tickets;
                    existingtbReunion.title = tiquetes.title;
                    existingtbReunion.id_user = tiquetes.id_user;
                    existingtbReunion.id_client = tiquetes.id_client;
                    existingtbReunion.estado = tiquetes.estado;
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
                var tiquete = ctx.tblSupport_Tickets
                    .Where(s => s.id_Support_Tickets == id)
                    .FirstOrDefault();

                ctx.Entry(tiquete).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}

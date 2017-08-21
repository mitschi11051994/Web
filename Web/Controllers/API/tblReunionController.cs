using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers.API
{
    public class tblReunionController : ApiController
    {
        public IHttpActionResult GetAlltblReunion(bool includeAddress = false)
        {
            IList<tblReunionViewModel> reunion = null;
            using (var ctx = new CMDEntities())
            {
                reunion = ctx.tblReunion.Select(s => new tblReunionViewModel()
                {
                    id_reunion= s.id_reunion,
                    title = s.title,
                    fecha_y_hora = (s.fecha_y_hora.Value),
                    id_user = s.id_user,
                    esVirtual = s.esVirtual.Value,
                    id_client= s.id_client.Value
                }).ToList<tblReunionViewModel>();
            }
            if (reunion.Count == 0)
            {
                return NotFound();
            }
            return Ok(reunion);
        }

        public IHttpActionResult GettblReunionById(int id)
        {
            tblReunionViewModel reunion = null;

            using (var ctx = new CMDEntities())
            {
                reunion = ctx.tblReunion.Where(s => s.id_reunion == id)
                    .Select(s => new tblReunionViewModel()
                    {
                        id_reunion = s.id_reunion,
                        title = s.title,
                        fecha_y_hora = (s.fecha_y_hora.Value),
                        id_user = s.id_user,
                        esVirtual = s.esVirtual.Value,
                        id_client = s.id_client.Value
                    }).FirstOrDefault<tblReunionViewModel>();
            }

            if (reunion == null)
            {
                return NotFound();
            }

            return Ok(reunion);
        }

        public IHttpActionResult GetAlltblContact(string title)
        {
            IList<tblReunionViewModel> reunion = null;

            using (var ctx = new CMDEntities())
            {
                reunion = ctx.tblReunion.Where(s => s.title.ToLower() == title.ToLower())
                    .Select(s => new tblReunionViewModel()
                    {
                        id_reunion = s.id_reunion,
                        title = s.title,
                        fecha_y_hora = (s.fecha_y_hora.Value),
                        id_user = s.id_user,
                        esVirtual = s.esVirtual.Value,
                        id_client = s.id_client.Value
                    }).ToList<tblReunionViewModel>();
            }
            if (reunion.Count == 0)
            {
                return NotFound();
            }
            return Ok(reunion);
        }

        //Get action methods of the previous section

        public IHttpActionResult PostNewtblContact(tblReunionViewModel reunion)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new CMDEntities())
            {
                ctx.tblReunion.Add(new tblReunion()
                {
                    id_reunion = reunion.id_reunion,
                    title = reunion.title,
                    fecha_y_hora = (reunion.fecha_y_hora),
                    id_user = reunion.id_user,
                    esVirtual = reunion.esVirtual,
                    id_client = reunion.id_client
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(tblReunionViewModel tblReunion)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CMDEntities())
            {
                var existingtbReunion = ctx.tblReunion.Where(s => s.id_reunion == tblReunion.id_reunion)
                                                        .FirstOrDefault<tblReunion>();

                if (existingtbReunion != null)
                {
                    existingtbReunion.id_reunion = tblReunion.id_reunion;
                    existingtbReunion.title = tblReunion.title;
                    existingtbReunion.id_user = tblReunion.id_user;
                    existingtbReunion.esVirtual = tblReunion.esVirtual;
                    existingtbReunion.id_client = tblReunion.id_client;
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
                var reunion = ctx.tblReunion
                    .Where(s => s.id_reunion == id)
                    .FirstOrDefault();

                ctx.Entry(reunion).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }



    }
}

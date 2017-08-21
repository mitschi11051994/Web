using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers.API
{
    public class tblContactController : ApiController
    {
        public IHttpActionResult GetAlltblContact(bool includeAddress = false)
        {
            IList<tblContactViewModel> contactos = null;
            using (var ctx = new CMDEntities())
            {
                contactos = ctx.tblContact.Select(s => new tblContactViewModel()
                {
                    id_contact = s.id_contact,
                    id_client = s.id_client.Value,
                    name = s.name,
                    first_name = s.first_name,
                    web_address = s.web_address,
                    tel = s.tel,
                    puesto = s.puesto
                }).ToList<tblContactViewModel>();
            }
            if (contactos.Count == 0)
            {
                return NotFound();
            }
            return Ok(contactos);
        }

        public IHttpActionResult GettblContactById(int id)
        {
            tblContactViewModel contactos = null;

            using (var ctx = new CMDEntities())
            {
                contactos = ctx.tblContact.Where(s => s.id_contact == id)
                    .Select(s => new tblContactViewModel()
                    {
                        id_contact = s.id_contact,
                        id_client = s.id_client.Value,
                        name = s.name,
                        first_name = s.first_name,
                        web_address = s.web_address,
                        tel = s.tel,
                        puesto = s.puesto
                    }).FirstOrDefault<tblContactViewModel>();
            }

            if (contactos == null)
            {
                return NotFound();
            }

            return Ok(contactos);
        }

        public IHttpActionResult GetAlltblContact(string name)
        {
            IList<tblContactViewModel> contactos = null;

            using (var ctx = new CMDEntities())
            {
                contactos = ctx.tblContact.Where(s => s.name.ToLower() == name.ToLower())
                    .Select(s => new tblContactViewModel()
                    {
                        id_contact = s.id_contact,
                        id_client = s.id_client.Value,
                        name = s.name,
                        first_name = s.first_name,
                        web_address = s.web_address,
                        tel = s.tel,
                        puesto = s.puesto
                    }).ToList<tblContactViewModel>();
            }
            if (contactos.Count == 0)
            {
                return NotFound();
            }
            return Ok(contactos);
        }

        //Get action methods of the previous section

        public IHttpActionResult PostNewtblContact(tblContactViewModel contacto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new CMDEntities())
            {
                ctx.tblContact.Add(new tblContact()
                {
                    id_contact = contacto.id_contact,
                    id_client = contacto.id_client,
                    name = contacto.name,
                    first_name = contacto.first_name,
                    web_address = contacto.web_address,
                    tel = contacto.tel,
                    puesto = contacto.puesto
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(tblContactViewModel tblContact)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CMDEntities())
            {
                var existingtblContact = ctx.tblContact.Where(s => s.id_contact == tblContact.id_contact)
                                                        .FirstOrDefault<tblContact>();

                if (existingtblContact != null)
                {
                    existingtblContact.id_contact = tblContact.id_contact;
                    existingtblContact.id_client = tblContact.id_client;
                    existingtblContact.name = tblContact.name;
                    existingtblContact.first_name = tblContact.first_name;
                    existingtblContact.web_address = tblContact.web_address;
                    existingtblContact.tel = tblContact.tel;
                    existingtblContact.puesto = tblContact.puesto;
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
                var contactos = ctx.tblContact
                    .Where(s => s.id_contact == id)
                    .FirstOrDefault();

                ctx.Entry(contactos).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }



    }
}

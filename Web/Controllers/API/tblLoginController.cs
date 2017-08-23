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
    public class tblLoginController : ApiController
    {
        public string Encript(string password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string Desencript(string password)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(password);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        
        
 
    public IHttpActionResult GetAlltblLogin(bool includeAddress = false)
        {
            IList<tblLoginViewModel> login = null;
            using (var ctx = new CMDEntities())
            {
                login = ctx.tblLogin.Select(s => new tblLoginViewModel()
                {
                    id_user= s.id_user,
                    Username = s.Username,
                    Password = (s.Password)
                }).ToList<tblLoginViewModel>();
            }
            if (login.Count == 0)
            {
                return NotFound();
            }
            return Ok(login);
        }

        public IHttpActionResult GetttblLogin(int id)
        {
            tblLoginViewModel login = null;

            using (var ctx = new CMDEntities())
            {
                login = ctx.tblLogin.Where(s => s.id_user == id)
                    .Select(s => new tblLoginViewModel()
                    {
                        id_user = s.id_user,
                        Username = s.Username,
                        Password = s.Password
                    }).FirstOrDefault<tblLoginViewModel>();
            }

            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        public IHttpActionResult GetAlltblLogin(string username)
        {
            IList<tblLoginViewModel> login = null;

            using (var ctx = new CMDEntities())
            {
                login = ctx.tblLogin.Where(s => s.Username.ToLower() == username.ToLower())
                    .Select(s => new tblLoginViewModel()
                    {
                        id_user = s.id_user,
                        Username = s.Username,
                        Password = s.Password
                    }).ToList<tblLoginViewModel>();
            }
            if (login.Count == 0)
            {
                return NotFound();
            }
            return Ok(login);
        }

        //Get action methods of the previous section

        public IHttpActionResult PostNewtblLogin(tblLoginViewModel tblLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new CMDEntities())
            {
                string encriptar = Encript(tblLogin.Password);

                ctx.tblLogin.Add(new tblLogin()
                {
                    
                    id_user= tblLogin.id_user,
                     Username= tblLogin.Username,
                     Password = encriptar,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(tblLoginViewModel tblLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CMDEntities())
            {
                var existingtblLogin = ctx.tblLogin.Where(s => s.id_user == tblLogin.id_user)
                                                        .FirstOrDefault<tblLogin>();

                if (existingtblLogin != null)
                {
                    existingtblLogin.id_user = tblLogin.id_user;
                    existingtblLogin.Username = tblLogin.Username;
                    existingtblLogin.Password = tblLogin.Password;
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
                var login = ctx.tblLogin
                    .Where(s => s.id_user == id)
                    .FirstOrDefault();

                ctx.Entry(login).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}

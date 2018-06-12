using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrBarber.Models;

namespace MrBarber.Controllers
{
    public class TermosController : Controller
    {
        private static Random random = new Random();
        private static int flag = 0;

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [HttpGet]
        public ActionResult Termos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Termos2()
        {
            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                if (TempData["cliente"] != null && flag==0)
                {

                    Cliente cliente = (Cliente) TempData["cliente"];
                    cliente.Pontos = 0;
                    cliente.CodigoProm = RandomString(5);

                    TempData["cliente"] = cliente;
                    TempData.Keep("cliente");
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    flag = 1;
                }
            }

            return View("Index3");
        }
        [HttpPost]
            public ActionResult Recusar()
             {
             return RedirectToAction("Index", "Home");
            }
        }
    }
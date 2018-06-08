using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrBarber.Models;

namespace MrBarber.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Registar()
        {

            Cliente cliente = new Cliente();

            return View(cliente);
        }

        [HttpPost]

        public ActionResult Registar(Cliente cliente)
        {

            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                Localizacao loc = new Localizacao
                {
                    LatitudeE = 0,
                    LatitudeO = 0,
                    LatitudeN = 0,
                    LatitudeS = 0,
                    idLocalizacao = 1
                };

                cliente.Localizacao = 1;
                db.Localizacaos.Add(loc);
                db.Clientes.Add(cliente);
                db.SaveChanges();
            }

            ModelState.Clear();

            return View("Registar",new Cliente());

            
        }



    }
}
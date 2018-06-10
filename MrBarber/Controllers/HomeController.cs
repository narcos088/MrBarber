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
   
            return View();
        }

        public ActionResult Contact()
        {

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
                db.Clientes.Add(cliente);
                db.SaveChanges();
            }

            ModelState.Clear();

            return View("Registar",new Cliente());

            
        }



    }
}
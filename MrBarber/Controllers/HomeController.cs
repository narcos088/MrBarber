using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public ActionResult Index2()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");
            return View(cliente);
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

        [HttpGet]
        public ActionResult Login()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login(string uname,string psw)
        {

            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                if (ModelState.IsValid)
                {
                    var clientes = (from m in db.Clientes
                                    where m.Email == uname && m.Password == psw
                                    select m);

                    if(clientes.ToList<Cliente>().Count> 0)
                    {

                        var myList = clientes.ToList<Cliente>();
                        Cliente cliente = myList.ElementAt(0);

                        TempData["cliente"] = cliente;
                        TempData.Keep("cliente");
                        return RedirectToAction("Perfil", "Perfil");
                    }
            }

            }

            return View("Index");
        }
    }
}
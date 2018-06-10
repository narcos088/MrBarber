using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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

            // var m = new Cliente{ idCliente = cliente.idCliente};
            TempData["cliente"] = cliente;
            return RedirectToAction("Termos", "Termos");
        }

    }
}
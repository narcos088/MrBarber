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

        public ActionResult Catalogo2()
        {
            return View();
        }

        public ActionResult Catalogo()
        {
            return View();
        }


        public ActionResult Colaborador(int id2)
        {
            TempData["idServico"] = id2;
            TempData.Keep("idServico");
            return RedirectToAction("Colaborador","Colaborador");
        }

        public ActionResult Logout()
        {
            return View("Index");
        }


        public double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double deg2radMultiplier = Math.PI / 180;
            lat1 = lat1 * deg2radMultiplier;
            lon1 = lon1 * deg2radMultiplier;
            lat2 = lat2 * deg2radMultiplier;
            lon2 = lon2 * deg2radMultiplier;

            double radius = 6378.137; // earth mean radius defined by WGS84
            double dlon = lon2 - lon1;
            double distance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(dlon)) * radius;

          
                return (distance);
           
        }


        [HttpGet]
        public ActionResult OndeEstamos()
        {
            return View("OndeEstamos");
        }

        [HttpPost]
        public ActionResult ConfirmaLoc(string Lat, string Lng)
        {
            if (Lat != "" && Lng != "")
            {
                double x = double.Parse(Lat, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(Lng, System.Globalization.CultureInfo.InvariantCulture);

                double distancia = Distance(x, y, 41.557813, -8.399161);


                ViewData["distancia"] = distancia;

                if (distancia <= 7)
                {
                    return View("OndeEstamos2");
                }

                else return View("OndeEstamos3");
            }
            else return View("OndeEstamos");
        }




        public ActionResult Politica()
        {

            return View();
        }

        public ActionResult Politica2()
        {

            return View();
        }



        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Contact2()
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

            TempData["cliente"] = cliente;
            TempData.Keep("cliente");
            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                if (ModelState.IsValid)
                {
                    if (cliente.CodigoAmigo != null)
                    {
                        var clientes = (from m in db.Clientes
                                        where m.CodigoProm == cliente.CodigoAmigo
                                        select m);

                        if (clientes.ToList<Cliente>().Count > 0)
                        {
                            return RedirectToAction("Termos", "Termos");
                        }
                        else return View("Registar2"); 

                    } else return RedirectToAction("Termos", "Termos");

                }
                return View("Registar2");
            }
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
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

        [HttpGet]
        public ActionResult OndeEstamos()
        {
            Localizacao localizacao = new Localizacao();

            
            
            TempData["localizacao"] = localizacao;
            TempData.Keep("localizacao");

            return View(localizacao);
        }

        [HttpPost]
        public ActionResult OndeEstamos2(string Lat, string Lng)
        {

            Localizacao localizacao = (Localizacao)TempData["localizacao"];
         /* localizacao.Latitude = decimal.Parse(Lat, System.Globalization.CultureInfo.InvariantCulture);
            localizacao.Longitude = System.Convert.ToDouble(Lng);*/

             double x = double.Parse(Lat, System.Globalization.CultureInfo.InvariantCulture);
             double y = double.Parse(Lng, System.Globalization.CultureInfo.InvariantCulture);

            localizacao.Latitude = (decimal)x;
            localizacao.Longitude = (decimal)y;

            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                db.Localizacaos.Add(localizacao);
                db.SaveChanges();
            }

            return View();
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
                    var clientes = (from m in db.Clientes
                                    where m.CodigoProm == cliente.CodigoAmigo
                                    select m);

                    if (clientes.ToList<Cliente>().Count > 0)
                    {
                        return RedirectToAction("Termos", "Termos");
                    }
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
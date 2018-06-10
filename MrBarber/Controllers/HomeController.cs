using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
<<<<<<< HEAD

            // var m = new Cliente{ idCliente = cliente.idCliente};
            TempData["cliente"] = cliente;
            return RedirectToAction("Termos", "Termos");
=======
            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            { 
                db.Clientes.Add(cliente);
                db.SaveChanges();
            }

            ModelState.Clear();

            return View("Registar",new Cliente());

            
>>>>>>> 18faa8a827dcdb8348e935e4faf2e06a4b51047f
        }
        [HttpGet]
        public ActionResult Login()
        {

<<<<<<< HEAD
=======
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
                        return View();
                    }
            }

            }
               

            //string username = null;

            /*
            SqlConnection sqlConnection1 = new SqlConnection("Data Source = DESKTOP - K682GFE; Initial Catalog = MrBarberDatabase; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework");

            using (sqlConnection1) {
                sqlConnection1.Open();

                SqlCommand cmd = new SqlCommand("SELECT Email FROM Cliente where Email=@uname",sqlConnection1);

                

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         username = reader.GetString(0);
                    }
                }

                sqlConnection1.Close();

            }
            */
           /* using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {


                 //db.Clientes.SqlQuery("SELECT Email FROM Cliente where Email=@uname", new SqlParameter("uname", uname));
                

                if ("1".Equals(uname)) { return View(); }
            }*/


            //if ("1".Equals(uname)) { return View(); }

            return View("Index");
        }
>>>>>>> 18faa8a827dcdb8348e935e4faf2e06a4b51047f
    }
}
using MrBarber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrBarber.Controllers
{
    public class HistoricoController : Controller
    {
        // GET: Historico
        [HttpGet]
        public ActionResult Historico()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");

            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {

                var agendamentos = (from m in db.Agendamentoes
                                where m.Cliente == cliente.idCliente
                                select m);

                if ((agendamentos.ToList<Agendamento>().Count > 0))
                {

                     var myList = agendamentos.ToList<Agendamento>();
                        return View(myList.AsEnumerable());
                }
            }
            return RedirectToAction("Perfil", "Perfil");
        }
    }
}
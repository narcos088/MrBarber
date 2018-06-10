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
            return View(cliente);
        }

        [HttpPost]
        public ActionResult VoltarPaginaInicial()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");
            //Aqui temos de criar uma pagina inical nova com o gajo logado, e com a opcao perfil no canto
            return RedirectToAction("Index2", "Home");
        }
    }
}
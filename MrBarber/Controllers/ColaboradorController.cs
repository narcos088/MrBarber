using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrBarber.Controllers
{
    public class ColaboradorController : Controller
    {
        // GET: Colaborador
        public ActionResult Colaborador()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Slot(int id2)
        {
            TempData["idColaborador"] = id2;
            TempData.Keep("idColaborador");
            return RedirectToAction("Slot", "Slot");
        }

    }
}
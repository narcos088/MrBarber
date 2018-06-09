﻿using MrBarber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrBarber.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil

        [HttpGet]
        public ActionResult Perfil()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");
            return View(cliente);
        }



        [HttpPost]
        public ActionResult Historico()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");
            return RedirectToAction("Historico", "Historico", cliente);
        }


        [HttpPost]
        public ActionResult Reclamacoes()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");
            return RedirectToAction("Reclamacoes", "Reclamacoes", cliente);
        }
    }
}
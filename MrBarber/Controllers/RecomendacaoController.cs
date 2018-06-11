using MrBarber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrBarber.Controllers
{
    public class RecomendacaoController : Controller
    {
        // GET: Reclamacoes
        [HttpGet]
        public ActionResult Recomendacao()
        {
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Concluir(string recomendacao)
        {
            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities()) { 
            Cliente cliente = (Cliente)TempData["cliente"];
            TempData.Keep("cliente");

            var clientes = (from m in db.Clientes
                            where m.Email == cliente.Email
                            select m);

                if ((clientes.ToList<Cliente>().Count > 0)){

                    var myList = clientes.ToList<Cliente>();
                    Cliente cliente2 = myList.ElementAt(0);
                    int id = cliente2.idCliente;
                    Recomendacao recomendacaoR = new Recomendacao { Descricao = recomendacao, idCliente = id };
                    db.Recomendacaos.Add(recomendacaoR);
                    db.SaveChanges();
                }
            }

           
            return RedirectToAction("Index2", "Home");
        }



    }
}
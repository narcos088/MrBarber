using MrBarber.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MrBarber.Controllers
{
    public class SlotController : Controller
    {

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


        // GET: Slot
        [HttpGet]
        public ActionResult PreSlot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PosSlot(string Lat, string Lng, string calendar)
        {

            if (Lat != "" && Lng != "" && calendar != "")
            {
                double x = double.Parse(Lat, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(Lng, System.Globalization.CultureInfo.InvariantCulture);
                double distancia = Distance(x, y, 41.557813, -8.399161);

                ViewData["distancia"] = distancia;



                DateTime thisDay = DateTime.Today.Date;

                ViewData["hoje"] = thisDay;

                string aux = DateTime.ParseExact(calendar, "yyyy-MM-dd", null).ToString("dd/MM/yyyy 00:00:00");

                DateTime dia = DateTime.ParseExact(aux, "dd/MM/yyyy 00:00:00", null);

                TempData["marcacao"] = dia;
                TempData.Keep("marcacao");

                int result = DateTime.Compare(dia, thisDay);

                if (distancia <= 7)
                {
                    if (result > 0)
                    {
                        List<int> a = new List<int>();

                        a = calculaSlot();

                        using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
                        {
                            Localizacao localizacao = new Localizacao{Latitude = (decimal)x, Longitude= (decimal)y };
                            TempData["Loc"] = localizacao;
                            TempData.Keep("Loc");
                            db.Localizacaos.Add(localizacao);
                            db.SaveChanges();
                        }

                            return View("Slot",a);
                    }

                    else return View("ErroData");
                }

                else return View("ErroLocal");

            }

            else return View("PreSlot");
        }

        public List<int> calculaSlot()
        {
            int func = (int)TempData["idColaborador"];
            TempData.Keep("idColaborador");
            List<int> ocupados = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                if (ModelState.IsValid)
                {
                    var agendamentos = (from m in db.Agendamentoes
                                        where m.Funcionario == func && m.Estado == "Agendado"
                                        select m.DataInicio);

                    foreach (var ag in agendamentos)
                    {
                        DateTime dataFunc = ag;

                        var hora = (int)dataFunc.Hour;

                        string data1 = dataFunc.ToString("dd/mm/yyyy");

                        DateTime dataAgend = (DateTime)TempData["marcacao"];

                        string data2 = dataAgend.ToString("dd/mm/yyyy");

                        bool result = data1.Equals(data2);

                        if (result == true)
                        {
                            if (hora == 9) ocupados[0] = 1;
                            if (hora == 10) ocupados[1] = 1;
                            if (hora == 11) ocupados[2] = 1;
                            if (hora == 13) ocupados[3] = 1;
                            if (hora == 14) ocupados[4] = 1;
                            if (hora == 15) ocupados[5] = 1;
                            if (hora == 16) ocupados[6] = 1;
                            if (hora == 17) ocupados[7] = 1;
                        }



                    }
                }

            }
        
            return ocupados;

        }



        [HttpGet]
        public ActionResult Slot()
        {
            List<int> a = new List<int>();

            a = calculaSlot();
            return View(a);
        }


        [HttpPost]
        public ActionResult Finalizar(string zero, string um, string dois, string tres, string quatro, string cinco, string seis, string sete)
        {

            using (MrBarberDatabaseEntities db = new MrBarberDatabaseEntities())
            {
                string hora = "";
                DateTime horaInserir = (DateTime)TempData["marcacao"];
                //Inserir Agendamento
                if (zero!="") hora = "09";
                if (um != "") hora = "10";
                if (dois != "") hora = "11";
                if (tres != "") hora = "13";
                if (quatro != "") hora = "14";
                if (cinco != "") hora = "15";
                if (seis != "") hora = "16";
                if (sete != "") hora = "17";
                string horaInserirString = horaInserir.ToString("yyyy/dd/MM ");

                StringBuilder builder = new StringBuilder();
                builder.Append(horaInserirString);
                builder.Append(hora);
                builder.Append(":00:00");


                DateTime oDate = DateTime.ParseExact(builder.ToString(), "yyyy/dd/MM HH:mm:ss", null);

                Cliente cliente = (Cliente)TempData["cliente"];
                int idCol = (int)TempData["idColaborador"];
                int idSer = (int)TempData["idServico"];
                Localizacao local = (Localizacao)TempData["Loc"];
            
                Agendamento agendamento = new Agendamento { DataInicio = oDate, Estado="Agendado",Cliente=cliente.idCliente,Funcionario=idCol,Servico=idSer,Reclamacao=null,Localizacao=local.idLocalizacao};
                db.Agendamentoes.Add(agendamento);
                db.SaveChanges();
            }

                return View();
        }


    }
}
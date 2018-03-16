using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet] //esta anotaçao é facultativa pois eh isto q acontece por defeito
        public ActionResult Index()
        {
            //inicializar o valor do visor 
            ViewBag.Visor = 0;
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            int op1;
            //identificar o valor da variavel 'bt'
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (visor.Equals("0")) visor = bt;
                    else visor += bt;

                
                    break;

                case "0":
                    if (!visor.Equals("0")) visor += bt;
                    break;

                case "+/-":
                    string pInt = "";
                    string pDec = "";
                    int i = 0;
                    while (!(visor[i] == ',') && i<visor.Length )
                    {
                        pInt += visor[i];
                        i++;
                    }
                    while(i < visor.Length )
                    {
                        pDec += visor[i];
                        i++;
                    }

                    bool res = int.TryParse(pInt, out op1);
                    op1 = op1 * (-1);
                    visor = op1.ToString();
                    visor += pDec;
                    break;

                case ",":
                    bool existe = false;

                    for (int j = 0; j < visor.Length; j++)
                        if (visor[j] == ',') existe = true;

                    if (!existe) visor += bt;  


                    break;

            }
            // enviar resposta para o cliente
            ViewBag.Visor = visor;
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        //nao funciona
        //public bool primeiroOperador = true;

        // GET: Home
        [HttpGet] //esta anotaçao é facultativa pois eh isto q acontece por defeito
        public ActionResult Index()
        {
            //inicializar o valor do visor 
            ViewBag.Visor = 0;
            // inicializar variaveis de formatação da calculadora 
            Session["primeiroOperador"] = true;
            //marcar o visor para limpeza
            Session["limpaVisor"] = true;

            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            
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
                case "0":
                    if ((bool)Session["limpaVisor"] || visor.Equals("0")) visor = bt;
                    else visor += bt;

                    //impedir o visor de ser limpo
                    Session["limpaVisor"] = false;



                    break;

                
                   

                case "+/-":
                    visor = Convert.ToDouble(visor) * -1 + "";
                    break;

                case ",":


                    if (!visor.Contains(","))
                        visor += bt;
                    break;

                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    //executar se NAO é a primeira vez que escolho um operador
                    if (!(bool)Session["primeiroOperador"]) {
                        
                    
                    
                        


                        //vars auxiliares
                        double operando1 = Convert.ToDouble((string)Session["operando"]);
                        double operando2 = Convert.ToDouble(visor);

                        switch ((string)Session["operadorAnterior"])
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                
                                
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                
                                
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                
                               
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                
                                
                                break;
                        }
                        

                    }
                    Session["primeiroOperador"] = false;
                    Session["operadorAnterior"] = bt;
                    //guardar o valor do operando para utilização futura
                    Session["operando"] = visor;
                    //marcar o visor para limpeza
                    Session["limpaVisor"] = true;

                    //tratar da situação do operador "="
                    if (bt.Equals("="))
                    {
                        Session["primeiroOperador"] = true;
                    }
                    break;
                case "C":
                    //limpa visor
                    visor = "0";
                    //reiniciar variaveis de formatação da calculadora 
                    Session["primeiroOperador"] = true;
                    //reiniciar o visor para limpeza
                    Session["limpaVisor"] = true;
                    break;
            } //switch (bt)

            // enviar resposta para o cliente
            ViewBag.Visor = visor;
            return View();
        }
    }
}
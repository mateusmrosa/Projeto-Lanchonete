using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PIT_tela_de_login.Controllers
{
    [Autorizacao]
    
    public class CadastrarProdutoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexPesquisar()
        {
            return View();
        }

        public JsonResult Gravar([FromBody]Dictionary<string, string> dados)
        {

            string msg = "";
            bool operacao = false;

            Models.Produto p = new Models.Produto();
            p.Nome = dados["nome"];
            p.TipoProd = Convert.ToInt32(dados["tipoProd"]);
            p.Quantidade = Convert.ToInt32(dados["quantidade"]);
            p.Valor = Convert.ToDouble(dados["valor"]);

            double valor = 0;
            if (double.TryParse(dados["valor"], out valor))
            {
                p.Valor = valor;
            }

            if (msg == "")
            {
                operacao = p.Gravar(p, out msg);

                if (operacao)
                {
                    msg = "Salvo com sucesso!";
                }
            }

            var retorno = new
            {
                operacao = operacao,
                msg = msg
            };

            return Json(retorno); //serializa....
        }
    }
}
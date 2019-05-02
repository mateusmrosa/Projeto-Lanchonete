using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PIT_tela_de_login.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Logar([FromBody]Dictionary<string, string> dados)
        {
            Models.Funcionario f = new Models.Funcionario();
            bool ok = f.ValidarSenha(dados["cpf"], dados["senha"]);

            string nome ="";

            if(ok == true)
            {
                f.Obter(dados["cpf"]);
                nome = f.NomeCompleto;
            }
            //obj anônimo
            var retornoServ = new
            {
                nome = nome,
                operacao = ok
            };

            return Json(retornoServ);
        }
    }
}
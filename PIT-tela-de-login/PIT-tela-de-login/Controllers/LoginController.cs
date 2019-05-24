using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

            if (ok)
            {
                f.Obter(dados["cpf"]);

                //criar a session
                HttpContext.Session.SetString("id", f.Id.ToString());
                HttpContext.Session.SetString("nome", f.NomeCompleto);
                
            }

            //string nome = "";

            //if (ok == true)
            //{
            //    f.Obter(dados["cpf"]);
            //    nome = f.NomeCompleto;
                
            //}

            //obj anônimo
            var retornoServ = new
            {
                nome = f.NomeCompleto,
                operacao = ok
            };

            return Json(retornoServ);
        }
    }
}
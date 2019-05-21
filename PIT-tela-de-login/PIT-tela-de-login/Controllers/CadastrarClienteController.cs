using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PIT_tela_de_login.Controllers
{
    public class CadastrarClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Gravar([FromBody] Dictionary<string, string> dados)
        {
            string msg = "";

            Models.Cliente c = new Models.Cliente();
            c.Nome = dados["nome"];
            c.Cpf = dados["cpf"];
            bool ok = c.Gravar(c, out msg);

            if (ok)
                msg = "<div class='alert alert-success'>Cadastro efetuado com sucesso!</div>";

            var obj = new
            {
                id = c.Id,
                operacao = ok,
                msg = msg
            };
           

            return Json(obj);
        }

        //public JsonResult Excluir([FromBody]Dictionary<string, string> dados)
        //{
        //    int id = Convert.ToInt32(dados["id"]);
        //    Models.Funcionario f = new Models.Funcionario();
        //    bool operacao = f.Excluir(id);

        //    return Json(new { operacao = operacao });

        //}
    }
}
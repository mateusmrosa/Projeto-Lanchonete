using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PIT_tela_de_login.Controllers
{
    public class CadastrarFuncionarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexPesquisar()
        {
            return View();
        }

        //public JsonResult Gravar([FromBody] Models.Funcionario f)
        //{

        //    System.Threading.Thread.Sleep(5000);

        //    //Deserializando os dados na model
        //    string msg = "";
        //    bool ok = f.Gravar(f, out msg);

        //    if (ok)
        //        msg = "Deu certo.";

        //    var obj = new
        //    {
        //        operacao = ok,
        //        msg = msg
        //    };

        //    return Json(obj);
        //}


        public JsonResult Gravar([FromBody] Dictionary<string, string> dados)
        {
            string msg = "";

            Models.Funcionario f = new Models.Funcionario();
            f.Id = Convert.ToInt32(dados["id"]);
            f.NomeCompleto = dados["nomeCompleto"];
            f.Senha = dados["senha"];
            f.Cpf = dados["cpf"];

            bool ok = f.Gravar(f, out msg);

            if (ok)
                msg = "<div class='alert alert-success'>Cadastro efetuado com sucesso!</div>";
                
            

            var obj = new
            {
                id = f.Id,
                operacao = ok,
                msg = msg
            };

            return Json(obj);
        }

        public JsonResult Pesquisar(string nome)
        {
            Models.Funcionario f = new Models.Funcionario();
            List<Models.Funcionario> funcs = f.Pesquisar(nome);

            return Json(funcs);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PIT_tela_de_login.Controllers
{
    [Autorizacao]
    public class RealizarPedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PesquisarProduto(string termo)
        {

            Models.Produto p = new Models.Produto();
            List<Models.Produto> lista = p.Pesquisar(termo);
            return Json(lista);
        }

        public JsonResult Gravar([FromBody] Dictionary<string, object> dados)
        {

            bool operacao = false;

            //if (HttpContext.Session.GetString("usuarioId") != null)

            int id = Convert.ToInt32(dados["id"]);
            int idFunc = Convert.ToInt32(dados["idfunc"]);
            Newtonsoft.Json.Linq.JArray items = (Newtonsoft.Json.Linq.JArray)dados["items"];

            Models.Pedido p = new Models.Pedido();
            p.Id = id;
            p.Funcionario.Id = idFunc;
            p.Data = DateTime.Now;
            //m.Funcionario
            p.Itens = new List<Models.PedidoItem>();

            foreach (Newtonsoft.Json.Linq.JObject item in items)
            {
                Models.PedidoItem pi = new Models.PedidoItem();
                pi.Quantidade = item.Value<int>("quantidade");
                pi.Valor = item.Value<double>("valor");
                pi.Produto = new Models.Produto();
                pi.Produto.Id = item.Value<int>("produtoId");
                p.Itens.Add(pi);
            }

            operacao = p.Gravar();


            return Json(new
            {
                operacao = operacao
            });
        }
    }
}
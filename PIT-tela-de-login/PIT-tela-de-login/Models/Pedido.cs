using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;


namespace PIT_tela_de_login.Models
{
    public class Pedido
    {
        private int _id;
        Funcionario _funcionario;
        DateTime _data;
        List<PedidoItem> _itens;

        public Pedido(int id, Funcionario funcionario, DateTime data, List<PedidoItem> itens)
        {
            Id = id;
            Funcionario = funcionario;
            Data = data;
            Itens = itens;
        }
        public Pedido()
        {
            Funcionario = new Funcionario();
        }

        public int Id { get => _id; set => _id = value; }
        public Funcionario Funcionario { get => _funcionario; set => _funcionario = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public List<PedidoItem> Itens { get => _itens; set => _itens = value; }



        public bool Gravar()
        {

            //DAL. MySQLPersistencia2 bd2 = DAL.MySQLPersistencia2.PegarInstancia();
            //bd2.Abrir();

            //oo-er
            bool retorno = false;

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();
            try
            {
                bd.IniciarTransacao();

                string sql1 = "insert into pedidos (data, idfuncionario) values(@data, @idfuncionario)";
                Dictionary<string, object> pSQL1 = new Dictionary<string, object>();
                pSQL1.Add("@data", _data);
                pSQL1.Add("@idfuncionario", Funcionario.Id);
                bd.ExecutarComando(sql1, pSQL1);
                int idPedidos = (int)bd.UltimoId;

                string sql2 = "insert into itens_pedido (idprodutos, valor, quantidade, idpedidos) values(@idprodutos, @valor, @quantidade, @idpedidos)";

                foreach (PedidoItem item in _itens)
                {
                    Dictionary<string, object> pSQL2 = new Dictionary<string, object>();
                    bd.LimparParametros();
                    pSQL2.Add("@idprodutos", item.Produto.Id);
                    pSQL2.Add("@valor", item.Valor);
                    pSQL2.Add("@quantidade", item.Quantidade);
                    pSQL2.Add("@idpedidos", idPedidos);

                    if (bd.ExecutarComando(sql2, pSQL2) == -1)
                    {
                        throw new Exception("Erro ao gravar.");
                    }
                }

                bd.CommitarTransacao();
                retorno = true;
            }
            catch (Exception ex)
            {
                //ex.Message
                retorno = false;
            }

            return retorno;
        }

    }
}

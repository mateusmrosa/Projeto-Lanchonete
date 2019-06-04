using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PIT_tela_de_login.Models
{
    public class Produto
    {
        private int _id;
        private string _nome;
        private int _tipoProd;
        private int _quantidade;
        private double _valor;

        public Produto(int id, string nome, int tipoProd, int quantidade, double valor)
        {
            Id = id;
            Nome = nome;
            TipoProd = tipoProd;
            Quantidade = quantidade;
            Valor = valor;
        }

        public Produto()
        {
            _id = 0;
            Nome = "";
            TipoProd = 0;
            Quantidade = 0;
            Valor = 0;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public int TipoProd { get => _tipoProd; set => _tipoProd = value; }
        public int Quantidade { get => _quantidade; set => _quantidade = value; }
        public double Valor { get => _valor; set => _valor = value; }

        public bool Gravar(Produto p, out string msg)
        {
            msg = "";

            //validacoes...

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();

            string sql = "";
            Dictionary<string, object> ps = new Dictionary<string, object>();

            if (p.Id == 0)
            {
                sql = @"insert into produtos (nome, tipo_prod, valor, quantidade) 
                         values (@nome, @tipo_prod, @valor, @quantidade)";
            }
            else
            {
                sql = @"update produtos 
                         set nome = @nome, tipo_prod = @tipo_prod, valor = @valor, quantidade = @quantidade
                         where idprodutos = @idprodutos";

                ps.Add("@idprodutos", p.Id);
            }

            ps.Add("@nome", p.Nome);
            ps.Add("@tipo_prod", p.TipoProd);
            ps.Add("@quantidade", p.Quantidade);
            ps.Add("@valor", p.Valor);

            int r = bd.ExecutarComando(sql, ps);
            if (r > 0 && p.Id == 0)
            {
                p.Id = Convert.ToInt32(bd.UltimoId);
            }
            return r == 1;
        }

        public List<Produto> Pesquisar(string termo)
        {

            List<Produto> lista = new List<Produto>();

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();

            string sql = "select * from produtos where lower(nome) like @nome";

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@nome", termo.ToLower() + "%");

            DataTable dt = bd.ExecutarConsulta(sql, ps);

            foreach (DataRow row in dt.Rows)
            {
                Produto p = new Produto();
                p.Id = Convert.ToInt32(row["idprodutos"]);
                p.Nome = row["nome"].ToString();
                p.TipoProd = Convert.ToInt32(row["tipo_prod"]);
                p.Valor = Convert.ToDouble(row["valor"]);
                p.Quantidade = Convert.ToInt32(row["quantidade"]);

                lista.Add(p);
            }

            return lista;

        }
    }


}

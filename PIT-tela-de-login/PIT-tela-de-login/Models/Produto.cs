using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIT_tela_de_login.Models
{
    public class Produto
    {
        private int _id;
        private string _nome;
        private int _tipoProd;
        private double _valor;

        public Produto(int id, string nome, int tipoProd, double valor)
        {
            Id = id;
            Nome = nome;
            TipoProd = tipoProd;
            Valor = valor;
        }

        public Produto()
        {
            _id = 0;
            Nome = "";
            TipoProd = 0;
            Valor = 0;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public int TipoProd { get => _tipoProd; set => _tipoProd = value; }
        public double Valor { get => _valor; set => _valor = value; }

        public bool Gravar(Produto p, out string msg)
        {
            msg = "";

            //validacoes...

            DAL.MySQLPersistencia bd = DAL.MySQLPersistencia.Conecta();

            string sql = "";
            Dictionary<string, object> ps = new Dictionary<string, object>();

            if (p.Id == 0)
            {
                sql = @"insert into produtos (nome, tipo_prod, valor) 
                         values (@nome, @tipo_prod, @valor)";
            }
            else
            {
                sql = @"update produtos 
                         set nome = @nome, tipo_prod = @tipo_prod, valor = @valor
                         where idprodutos = @idprodutos";

                ps.Add("@id", p.Id);
            }

            ps.Add("@nome", p.Nome);
            ps.Add("@tipo_prod", p.TipoProd);
            ps.Add("@valor", p.Valor);

            int r = bd.ExecutarComando(sql, ps);
            if (r > 0 && p.Id == 0)
            {
                p.Id = Convert.ToInt32(bd.UltimoId);
            }
            return r == 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIT_tela_de_login.Models
{
    public class Cliente
    {
        private int _id;
        private string _nome;
        private string _cpf;

        public Cliente(int id, string nome, string cpf)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }


        public Cliente()
        {
            Id = 0;
        }

        public bool Gravar(Cliente c, out string msg)
        {
            msg = "";

            //if (c.Cpf.Length < 11)
            //{
            //    msg = "<div class='alert alert-danger'>CPF inválido.</div>";
            //    return false;
            //}

            //if (CPFCadastrado(c.Cpf, c.Id))
            //{
            //    msg = "<div class='alert alert-danger'>Este CPF já foi cadastrado.</div>";
            //    return false;
            //}

            DAL.MySQLPersistencia bd = DAL.MySQLPersistencia.Conecta();

            string sql = "";
            Dictionary<string, object> ps = new Dictionary<string, object>();

            if (c.Id == 0)
            {
                sql = @"insert into clientes (nome, cpf) 
                         values (@nome, @cpf)";
            }
            else
            {
                sql = @"update clientes 
                         set nome = @nome, cpf = @cpf
                         where idclientes = @id";

                ps.Add("@id", c.Id);
            }

            ps.Add("@nome", c.Nome);
            ps.Add("@cpf", c.Cpf);

            int r = bd.ExecutarComando(sql, ps);
            if (r > 0 && c.Id == 0)
            {
                c.Id = Convert.ToInt32(bd.UltimoId);
            }
            return r == 1;
        }

        public bool CPFCadastrado(string cpf, int idDonoCPF)
        {

            DAL.MySQLPersistencia bd = DAL.MySQLPersistencia.Conecta();

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@cpf", cpf);
            ps.Add("@idDonoCPF", idDonoCPF);

            string sql = @"select count(*) from clientes
                           where cpf = @cpf and idclientes <> @idDonoCPF";

            Int64 valor = (Int64)bd.ExecutarAgregacao(sql, ps);

            return valor == 1;
        }

        //public bool Excluir(int id)
        //{
        //    DAL.MySQLPersistencia bd = DAL.MySQLPersistencia.Conecta();
        //    string sql = "delete from clientes where id = @id";
        //    Dictionary<string, object> ps = new Dictionary<string, object>();
        //    ps.Add("@id", id);

        //    int linhasAfetadas = bd.ExecutarComando(sql, ps);
        //    return linhasAfetadas > 0;
        //}
    }
}

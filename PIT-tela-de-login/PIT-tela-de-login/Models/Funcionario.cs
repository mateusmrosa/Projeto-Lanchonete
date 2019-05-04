using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PIT_tela_de_login.Models
{
    public class Funcionario
    {
        private int _id;
        private string _nomeCompleto;
        string _cpf;
        string _senha;

        public int Id { get => _id; set => _id = value; }
        public string NomeCompleto { get => _nomeCompleto; set => _nomeCompleto = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }
        public string Senha { get => _senha; set => _senha = value; }

        //public void SetId(int id)
        //{
        //    Id = id;
        //}

        //public int GetId()
        //{
        //    return Id;
        //}


        public Funcionario()
        {
            Id = 0;
        }

        public Funcionario(int id, string nomeCompleto, string cpf, string senha)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            Senha = senha;

        }

        public bool Gravar(Funcionario f, out string msg)
        {

            msg = "";

            if (f.Cpf.Length < 11)
            {
                msg = "<div class='alert alert-danger'>CPF inválido.</div>";
                return false;
            }

            if(f.Id == 0)
            {
                if (f.Senha.Length < 6)
                {
                    msg = "<div class='alert alert-danger'>Senha muito pequena.</div>";
                    return false;
                }
            }

            if (CPFCadastrado(f.Cpf, f.Id))
            {
                msg = "<div class='alert alert-danger'>Este CPF já foi cadastrado.</div>";
                return false;

            }


            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();

            string sql = "";
            Dictionary<string, object> ps = new Dictionary<string, object>();

            if (f.Id == 0)
            {
                sql = @"insert into funcionario (nome, cpf, senha) 
                         values (@nome, @cpf, @senha)";
            }
            else
            {
                sql = @"update funcionario 
                         set nome = @nome, cpf = @cpf, senha = @senha
                         where id = @id";

                ps.Add("@id", f.Id);
            }

            ps.Add("@nome", f.NomeCompleto);
            ps.Add("@cpf", f.Cpf);
            ps.Add("@senha", f.Senha);

            int r = bd.ExecutarComando(sql, ps);
            if (r > 0 && f.Id == 0)
            {
                f.Id = Convert.ToInt32(bd.UltimoId);
            }
            return r == 1;
        }

        public bool CPFCadastrado(string cpf, int idDonoCPF)
        {

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@cpf", cpf);
            ps.Add("@idDonoCPF", idDonoCPF);

            string sql = @"select count(*) from funcionario
                           where cpf = @cpf and id <> @idDonoCPF";

            Int64 valor = (Int64)bd.ExecutarAgregacao(sql, ps);

            return valor == 1;
        }

        public bool ValidarSenha(string cpf, string senha)
        {
            
            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();

            string sql = @"select count(*) from funcionario 
                           where cpf = @cpf and senha = @senha";

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@cpf", cpf);
            ps.Add("@senha", senha);

            Int64 valor = (Int64)bd.ExecutarAgregacao(sql, ps);

            if (valor == 1)
            {
                return true;
            }
            else return false;
        }

        public List<Funcionario> Pesquisar(string nome)
        {

            List<Funcionario> funcs = new List<Funcionario>();

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();
            string sql = "select * from funcionario where nome like @nome";

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@nome", nome + "%");

            DataTable dt = bd.ExecutarConsulta(sql, ps);
            //Mapeando linhas de tabela em objetos.

            foreach (DataRow linha in dt.Rows)
            {
                Funcionario f = new Funcionario();

                f.Id = Convert.ToInt32(linha["id"]);
                f.NomeCompleto = linha["nome"].ToString();
                f.Cpf = linha["cpf"].ToString();
                f.Senha = linha["senha"].ToString();

                funcs.Add(f);
            }

            return funcs;
        }

        

        public Funcionario Obter(string cpf)
        {
            Funcionario f = new Funcionario();

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();
            string sql = "select * from funcionario where cpf = @cpf";

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@cpf", cpf);

            DataTable dt = bd.ExecutarConsulta(sql, ps);
            //Mapeando linhas de tabela em objetos.

            f.Id = Convert.ToInt32(dt.Rows[0]["id"]);
            f.NomeCompleto = dt.Rows[0]["nome"].ToString();
            f.Cpf = dt.Rows[0]["cpf"].ToString();
            f.Senha = dt.Rows[0]["senha"].ToString();

            return f;
        }


        public Funcionario Obter(int id)
        {
            Funcionario f = new Funcionario();

            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();
            string sql = "select * from funcionario where id = @id";

            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@id", id);

            DataTable dt = bd.ExecutarConsulta(sql, ps);
            //Mapeando linhas de tabela em objetos.

            f.Id = Convert.ToInt32(dt.Rows[0]["id"]);
            f.NomeCompleto = dt.Rows[0]["nome"].ToString();
            f.Cpf = dt.Rows[0]["cpf"].ToString();
            f.Senha = dt.Rows[0]["senha"].ToString();

            return f;
        }

        public bool Excluir(int id)
        {
            DAL.MySQLPersistencia bd = new DAL.MySQLPersistencia();
            string sql = "delete from funcionario where id = @id";
            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("@id", id);

            int linhasAfetadas = bd.ExecutarComando(sql, ps);
            return linhasAfetadas > 0;
        }
    }


}

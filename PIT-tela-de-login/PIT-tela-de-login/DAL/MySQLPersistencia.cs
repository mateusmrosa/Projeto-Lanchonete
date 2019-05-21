using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PIT_tela_de_login.DAL
{
    public class MySQLPersistencia
    {
        private static MySQLPersistencia instancia;
        private MySqlConnection _conexao;
        MySqlCommand _cmd;
        long _ultimoId = 0;
        string _msgErro = "";
        string _msgErroTecnica = "";

        public long UltimoId { get => _ultimoId; }
        public string MsgErro { get => _msgErro; }
        public string MsgErroTecnica { get => _msgErroTecnica; }

        private MySQLPersistencia()
        {
            string strcon = "Server=den1.mysql6.gear.host;Database=pitsyslan;Uid=pitsyslan;Pwd=$123456;";
            _conexao = new MySqlConnection(strcon);
            _cmd = _conexao.CreateCommand();

        }

        public static MySQLPersistencia Conecta()
        {
            if (instancia == null)
            {
                instancia = new MySQLPersistencia();
            }
            return instancia;
        }
        /// <summary>
        /// Abre a conexão...
        /// </summary>
        public void Abrir()
        {
            if (_conexao.State != System.Data.ConnectionState.Open)
                _conexao.Open();
        }

        /// <summary>
        /// Fechar a conexão.
        /// </summary>
        public void Fechar()
        {
            _conexao.Close();
            _cmd.Parameters.Clear();
        }


        /// <summary>
        /// Usado para executar comandos Insert, Delete e Update, além de executar Stored Procedure.
        /// </summary>
        /// <param name="comando">Comando a ser executado</param>
        public int ExecutarComando(string comando)
        {

            int linhaAfetadas = 0;
            _cmd.CommandText = comando;

            Abrir();

            try
            {
                //tenta
                linhaAfetadas = _cmd.ExecuteNonQuery();
                _ultimoId = _cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                //erro
                _msgErroTecnica = ex.Message;
                _msgErro = "Não foi possível executar.";
                linhaAfetadas = -1;
            }
            finally
            {
                //sempre passará por aqui (no erro ou no acerto)
                Fechar();
            }

            return linhaAfetadas;

        }


        /// <summary>
        /// Usado para executar comandos Insert, Delete e Update, além de executar Stored Procedure.
        /// </summary>
        /// <param name="comando">Comando a ser executado</param>
        /// <param name="parametros">Lista e parâmetros para o comando.</param>
        public int ExecutarComando(string comando,
            Dictionary<string, object> parametros)
        {

            int linhaAfetadas = 0;
            _cmd.CommandText = comando;

            foreach (var item in parametros)
            {
                _cmd.Parameters.AddWithValue(item.Key, item.Value);
            }

            Abrir();

            try
            {
                //tenta
                linhaAfetadas = _cmd.ExecuteNonQuery();
                _ultimoId = _cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                //erro
                _msgErroTecnica = ex.Message;
                _msgErro = "Não foi possível executar.";
                linhaAfetadas = -1;
            }
            finally
            {
                //sempre passará por aqui (no erro ou no acerto)
                Fechar();
            }

            return linhaAfetadas;

        }

        public DataTable ExecutarConsulta(string comando)
        {
            DataTable dt = new DataTable();

            _cmd.CommandText = comando;

            Abrir();
            try
            {
                dt.Load(_cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                _msgErroTecnica = ex.Message;
                _msgErro = "Não foi possível obter os dados.";
            }
            finally
            {
                Fechar();
            }

            return dt;
        }

        public DataTable ExecutarConsulta(string comando, Dictionary<string, object> parametros)
        {
            DataTable dt = new DataTable();

            _cmd.CommandText = comando;
            foreach (var item in parametros)
            {
                _cmd.Parameters.AddWithValue(item.Key, item.Value);
            }

            Abrir();
            try
            {
                dt.Load(_cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                _msgErroTecnica = ex.Message;
                _msgErro = "Não foi possível obter os dados.";
            }
            finally
            {
                Fechar();
            }

            return dt;
        }

        public object ExecutarAgregacao(string comando)
        {
            object valor = null;

            _cmd.CommandText = comando;

            Abrir();
            try
            {
                valor = _cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _msgErroTecnica = ex.Message;
                _msgErro = "Não foi possível obter os dados.";
            }
            finally
            {
                Fechar();
            }

            return valor;
        }

        public object ExecutarAgregacao(string comando, Dictionary<string, object> parametros)
        {
            object valor = null;

            _cmd.CommandText = comando;
            foreach (var item in parametros)
            {
                _cmd.Parameters.AddWithValue(item.Key, item.Value);
            }

            Abrir();
            try
            {
                valor = _cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _msgErroTecnica = ex.Message;
                _msgErro = "Não foi possível obter os dados.";
            }
            finally
            {
                Fechar();
            }

            return valor;
        }

    }

}


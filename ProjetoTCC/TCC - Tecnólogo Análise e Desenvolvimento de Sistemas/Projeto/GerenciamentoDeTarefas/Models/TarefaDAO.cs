using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using GerenciamentoDeTarefas.Models;
using GerenciamentoDeTarefas.ConstantesSQL;
using System.Data;

namespace GerenciamentoDeTarefas.Models
{
    /**
     * Autor: Lucas de Mello
     * Essa classe é responsável pelos métodos de persistência das tarefas do sistema.
     * **/
    public class TarefaDAO
    {
        /**
         * Definindo os atributos a serem utilizados
         * **/
        private OleDbConnection conn;
        private List<Tarefa> listaDeTarefas;
        private DataSet ds;
        private OleDbDataAdapter da;
        private DataTable dt;

        /**
         * Autor: Lucas de Mello
         * Criando o construtor da classe
         * **/
        public TarefaDAO() {
            //-- reinicia a lista de tarefas --//
            this.listaDeTarefas = null;
        }

        /**
         * Autor: Lucas de Mello
         * Metodo utilizado para incluir nova tarefa na base de dados access
         * **/
        public bool incluirNovaTarefa(Tarefa obj) {
            bool fez = false;
            conn = null;
            try {
                conn = ConexaoAccess.getConexaoAccess();
                OleDbCommand cmm = new OleDbCommand(TarefasConstantesSQL.getIncluirNovaTarefa(obj), conn);
                cmm.ExecuteNonQuery();

                fez = true;

            } catch (Exception e) {
                ConexaoAccess.getFecharConexao(conn);

                Console.WriteLine(e.Message);

            } finally {
                ConexaoAccess.getFecharConexao(conn);
            }
            return fez;
        }

        /**
         * Autor: Lucas de Mello
         * Metodo utilizado para alterar os dados da tarefa na base de dados access
         * **/
        public bool alterarDadosTarefa(Tarefa obj) {
            bool fez = false;
            conn = null;
            try
            {
                conn = ConexaoAccess.getConexaoAccess();
                OleDbCommand cmm = new OleDbCommand(TarefasConstantesSQL.getAlterarDadosTarefa(obj), conn);
                cmm.ExecuteNonQuery();

                fez = true;

            }
            catch (Exception e)
            {
                ConexaoAccess.getFecharConexao(conn);

                Console.WriteLine(e.Message);

            }
            finally
            {
                ConexaoAccess.getFecharConexao(conn);
            }
            return fez;
        }

        /**
         * Autor: Lucas de Mello
         * Metodo utilizado para excluir uma determinada tarefa na base de dados access
         * **/
        public bool excluirTarefaPorId(int id)
        {
            bool fez = false;
            conn = null;
            try
            {
                conn = ConexaoAccess.getConexaoAccess();
                OleDbCommand cmm = new OleDbCommand(TarefasConstantesSQL.getExcluirTarefaPorId(id), conn);
                cmm.ExecuteNonQuery();

                fez = true;

            }
            catch (Exception e)
            {
                ConexaoAccess.getFecharConexao(conn);

                Console.WriteLine(e.Message);

            }
            finally
            {
                ConexaoAccess.getFecharConexao(conn);
            }
            return fez;
        }

        /**
         * Autor: Lucas de Mello
         * Recuperar todas as tarefas ordenadas por nome
         * **/
        public List<Tarefa> recuperarTodasTarefas() {
            listaDeTarefas = null;
            conn = null;
            ds = null;
            dt = null;
            try {
                ds = new DataSet();
                conn = ConexaoAccess.getConexaoAccess();
                da = new OleDbDataAdapter(TarefasConstantesSQL.recuperarTodasTarefas(), conn);

                da.Fill(ds);

                dt = ds.Tables[0];

                listaDeTarefas = new List<Tarefa>();

                foreach (DataRow rows in dt.Rows) {
                    Tarefa t = new Tarefa();
                    t.Data = DateTime.Parse(rows["data"].ToString());
                    t.Descricao = rows["descricao"].ToString();
                    t.id = int.Parse(rows["id"].ToString());
                    t.Nome = rows["nome"].ToString();
                    t.Status = rows["status"].ToString();

                    listaDeTarefas.Add(t);
                }

            } catch (Exception e) {
                ConexaoAccess.getFecharConexao(conn);
                Console.WriteLine(e.Message);

            } finally {
                
                ConexaoAccess.getFecharConexao(conn);
            }

            return listaDeTarefas;
        }

        /**
         * Autor: Lucas de Mello
         * Método utilizado para obter uma determinada tarefa por id
         * **/
        public Tarefa recuperarTarefaPorId(int id) {
            Tarefa t = new Tarefa();
            conn = null;
            ds = null;
            dt = null;
            try {
                ds = new DataSet();

                conn = ConexaoAccess.getConexaoAccess();

                da = new OleDbDataAdapter(TarefasConstantesSQL.recuperarTarefaPorId(id), conn);

                da.Fill(ds);

                dt = ds.Tables[0];

                t.id = int.Parse(dt.Rows[0]["id"].ToString());
                t.Data = DateTime.Parse(dt.Rows[0]["data"].ToString());
                t.Data.ToString("dd/MM/yyyy");
                t.Descricao = dt.Rows[0]["descricao"].ToString();
                t.Nome = dt.Rows[0]["nome"].ToString();
                t.Status = dt.Rows[0]["status"].ToString();

            } catch (Exception e) {
                ConexaoAccess.getFecharConexao(conn);
                Console.WriteLine(e.Message);

            } finally {
                ConexaoAccess.getFecharConexao(conn);
            }

            return t;
        }

        /**
         * Autor: Lucas de Mello
         * Recuperar todas as tarefas por parte do nome
         * **/
        public List<Tarefa> recuperarTodasTarefasPorParteNome(string nome)
        {
            listaDeTarefas = null;
            conn = null;
            ds = null;
            dt = null;
            try
            {
                conn = ConexaoAccess.getConexaoAccess();
                da = new OleDbDataAdapter(TarefasConstantesSQL.recuperarTarefaPorParteNome(nome), conn);

                ds = new DataSet();

                da.Fill(ds, "TabTarefas");

                dt = ds.Tables[0];

                listaDeTarefas = new List<Tarefa>();

                foreach (DataRow rows in dt.Rows)
                {
                    Tarefa t = new Tarefa();
                    t.Data = DateTime.Parse(rows["data"].ToString());
                    t.Descricao = rows["descricao"].ToString();
                    t.id = int.Parse(rows["id"].ToString());
                    t.Nome = rows["nome"].ToString();
                    t.Status = rows["status"].ToString();

                    listaDeTarefas.Add(t);
                }

            }
            catch (Exception e)
            {
                ConexaoAccess.getFecharConexao(conn);
                Console.WriteLine(e.Message);

            }
            finally
            {

                ConexaoAccess.getFecharConexao(conn);
            }

            return listaDeTarefas;
        }

    }
}
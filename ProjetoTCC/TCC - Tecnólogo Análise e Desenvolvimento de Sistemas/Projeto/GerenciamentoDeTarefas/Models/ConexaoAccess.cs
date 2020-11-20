using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace GerenciamentoDeTarefas.Models
{
    /**
     * Autor: Lucas de Mello
     * Essa classe é responsável por gerenciar as conexões do sistema com o banco de dados access
     * **/
    public class ConexaoAccess
    {
        /**
         * Autor: Lucas de Mello
         * Esse método é responsável por abrir uma nova conexão com o banco de dados access
         * **/
        public static OleDbConnection getConexaoAccess() {
            OleDbConnection conexao = null;

            try {
                    conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\Projeto\\BacoDeDadosAccess\\TarefaDB.mdb");
                    conexao.Open();

            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
            
            return conexao;
        }

        /**
         * Autor: Lucas de Mello
         * Esse métodop é responsável por fechar a conexão com o banco de dados access.
         * **/
        public static void getFecharConexao(OleDbConnection conn)
        {
            try
            {
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }      
    }
}
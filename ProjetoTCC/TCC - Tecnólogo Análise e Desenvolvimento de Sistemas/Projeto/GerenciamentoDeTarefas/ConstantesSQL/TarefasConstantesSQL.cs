using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GerenciamentoDeTarefas.Models;

namespace GerenciamentoDeTarefas.ConstantesSQL
{
    /**
     * Autor: Lucas de Mello     
     * Classe responsavel pelas constantes SQL do sistema
     * **/
    public class TarefasConstantesSQL
    {
        /**
         * Definindo as constantes do sistema para adicionar na persistencia das tarefas
         * Autor: Lucas de Mello         
         * **/
        public static string getIncluirNovaTarefa(Tarefa obj) {
            string SQL = "INSERT INTO TabTarefas(nome, descricao, data, status) VALUES ('"+obj.Nome+"', '"+obj.Descricao+"', '"+obj.Data+
                         "', '"+obj.Status+"')";

            return SQL;
        }

        public static string getAlterarDadosTarefa(Tarefa obj) {
            string SQL = "UPDATE TabTarefas SET nome = '"+obj.Nome+"', descricao = '"+obj.Descricao+"', data = '"+obj.Data+
                         "', status = '"+obj.Status+"' WHERE id = "+obj.id;

            return SQL;
        }

        public static string getExcluirTarefaPorId(int id) {
            string SQL = "DELETE FROM TabTarefas WHERE id= "+id;

            return SQL;
        }

        public static string recuperarTarefaPorId(int id) {
            string SQL = "SELECT * FROM TabTarefas WHERE id = "+id;

            return SQL;
        }

        public static string recuperarTodasTarefas() {
            string SQL = "SELECT * FROM TabTarefas ORDER BY nome";

            return SQL;
        }

        public static string recuperarTarefaPorParteNome(string nome) {
            string SQl = "SELECT TabTarefas.id, TabTarefas.nome, TabTarefas.descricao, TabTarefas.data, TabTarefas.status "+
                         "FROM TabTarefas "+
                         "WHERE TabTarefas.nome LIKE '%"+nome+"%' "+
                         "ORDER BY TabTarefas.nome";

            return SQl;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciamentoDeTarefas.Models
{
    /**
     * Autor: Lucas de Mello
     * Classe responsavel por gerenciar as opcoes do status
     * **/
    public class OpcoesStatus
    {
        public string Status { get; set; }
        public string Nome { get; set; } 

        public List<OpcoesStatus> ListaOpcoes()
        {
            return new List<OpcoesStatus>
            {
                new OpcoesStatus { Status = "Em atraso", Nome = "Em atraso"},
                new OpcoesStatus { Status = "Em andamento", Nome = "Em andamento"},
                new OpcoesStatus { Status = "Concluído", Nome = "Concluído"}
            };

        }

    }

}
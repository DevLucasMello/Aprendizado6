using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace GerenciamentoDeTarefas.Models
{
    public class SearchUsuario
    {
        public IPagedList<Tarefa> lista { get; set; }

        public SearchUsuario()
        {
            lista = new List<Tarefa>().ToPagedList(1, 50);
        }
    }
}
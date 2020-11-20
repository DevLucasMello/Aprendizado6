using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeTarefas.Models
{
    /**
     * Autor: Lucas de Mello
     * Essa classe é responsável pelos atributos e métodos acessores das tarefas
     * **/
    public class Tarefa
    {
        /**
         * Definindo os atributos e seus métodos acessores
         * Lucas de Mello
         * **/
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Informe o nome é obrigatório.")]
        public string Nome { get; set;}

        [Required(ErrorMessage = "Informe a descrição é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o status é obrigatório.")]
        public string Status { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Informe a data é obrigatório.")]
        public DateTime Data { get; set; }

    }
}
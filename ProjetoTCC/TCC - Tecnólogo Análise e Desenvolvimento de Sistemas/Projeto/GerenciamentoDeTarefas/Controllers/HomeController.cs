using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GerenciamentoDeTarefas.Models;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeTarefas.Controllers
{
    public class HomeController : Controller
    {
        /**
        * Definindo os atributos a serem utilizados
        * **/
        private OleDbConnection conn;

        /**
         * Autor: Lucas de Mello         
         * Definindo a grade de registros e as paginacoes 
         * **/
        public ViewResult Index(int? page, string SearchString)
        {
            //-- Definindo o Título da página principal para enviar para view home --//
            ViewBag.titulo = "Gerenciamento de Tarefas Acadêmicas";

            //-- definindo o tamanho de paginas a serem exibidas --//
            int tamanhpoPagina = 5;

            //-- definindo o numero de pagina e se o valor for nulo a pagina sera um  --//
            int numeroPagina = page ?? 1;

            //-- verifica se existe mensagem de sucesso do cadastro realizado - crud --//
            if (ViewBag.sucesso = TempData["sucesso"] != null) {
                ViewBag.sucesso = TempData["sucesso"].ToString();

                TempData["sucesso"] = null;
            }

            SearchUsuario search = new SearchUsuario();

            if (!string.IsNullOrEmpty(SearchString))
            {
                search.lista = new TarefaDAO().recuperarTodasTarefasPorParteNome(SearchString).ToPagedList(numeroPagina, tamanhpoPagina);
            }
            else
            {
                
                search.lista = new TarefaDAO().recuperarTodasTarefas().ToPagedList(numeroPagina, tamanhpoPagina);
            }

            return View(search);
        }

        /**
         * Autor: Lucas de Mello         
         * Metodo usado para realizar o cadastramento das tarefas
         * **/
        public ActionResult CadastrarTarefas(Tarefa obj)
        {
            //-- definindo o titulo e o subtitulo da pagina --//
            ViewBag.titulo = "ADICIONAR NOVA TAREFA";
            ViewBag.subTitulo = "Atenção! Informe os dados abaixo:";


            // Criando uma ViewBag com uma lista de clientes.
            // Utilizo o nome da ViewBag com ClienteId apenas
            // para facilitar o entendimento do código
            // new SelectList significa retornar uma
            // estrutura de DropDownList
            ViewBag.Status = new SelectList
            (
                new OpcoesStatus().ListaOpcoes(),
                "Status",
                "Nome"
            );


            //-- se a tarefa foi preenchida realiza o cadastro --//
            if (obj != null && obj.Nome != null) {
                conn = null;
                bool fez = false;
                try
                {
                    conn = ConexaoAccess.getConexaoAccess();
                    fez = new TarefaDAO().incluirNovaTarefa(obj);

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

                //-- verificando se o cadasdtro foi feito com sucesso ou não --//
                if (fez) {
                    Session["sucesso"] = "Atenção! A nova tarefa foi cadastrada com sucesso.";

                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.erro = "Atenção! Não foi possível cadastrar a nova tarefa, operação inválida.";
                }
            }

            return View();
        }

        /**
         * Sobrescrita de métodos para tratar o envio do que foi selecionado no comboox de status
         * Autor: Lucas de Mello         
         * **/
        [HttpPost]
        public ActionResult CadastrarTarefas(Tarefa obj, string Status)
        {
            //-- definindo o titulo e o subtitulo da pagina --//
            ViewBag.titulo = "ADICIONAR NOVA TAREFA";
            ViewBag.subTitulo = "Atenção! Informe os dados abaixo:";

            // O quarto parametro "clienteId" diz qual é o ID
            // que deve vir pré-selecionado ao montar o DropDownList
            ViewBag.Status = new SelectList
            (
                new OpcoesStatus().ListaOpcoes(),
                "Status",
                "Nome",
                Status
            );

            //-- se a tarefa foi preenchida realiza o cadastro --//
            if (obj != null && obj.Nome != null)
            {
                conn = null;
                bool fez = false;
                try
                {
                    conn = ConexaoAccess.getConexaoAccess();
                    fez = new TarefaDAO().incluirNovaTarefa(obj);

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

                //-- verificando se o cadasdtro foi feito com sucesso ou não --//
                if (fez)
                {
                    Session["sucesso"] = "Atenção! A nova tarefa foi cadastrada com sucesso.";

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.erro = "Atenção! Não foi possível cadastrar a nova tarefa, operação inválida.";
                }
            }

            return View();
        }

        /**
         * Autor: Lucas de Mello         
         * Dados do responsável pelo sistema.
         * **/
        public ActionResult Contact()
        {
            ViewBag.titulo = "Gerenciamento de Tarefas Acadêmicas";
            ViewBag.subTitulo = "Olá! Seja bem vindo, estamos à disposição para realizar o seu atedimento, entre em contato, clique sobre o link do e-mail abaixo.";
            ViewBag.email = "lucas.sports@hotmail.com";
            ViewBag.sobre = "O sistema de gerenciamento de tarefas acadêmicas, tem como objetivo controlar as suas tarefas, através de três itens principais. "+
                            "Status: Tarefas {Em andamento}, Tarefas {Em atraso} e Tarefas {Concluídas}, as regras poderão se atualizadas de acordo com o "+
                            "Administrador do sistema.";

            return View();
        }

        /**
         * Autor: Lucas de Mello         
         * Visualizar dados da tarefa
         * **/
        public ActionResult Visualizar(int id) {
            //-- definindo os dados da página de vizualização  --//
            ViewBag.titulo = "VISUALIZAR DADOS DA TAREFA";
            ViewBag.subTitulo = "Detalhes Abaixo:";

            //-- recuperando a tarefa selecionada por id --//
            Tarefa t = new TarefaDAO().recuperarTarefaPorId(id);
            ViewBag.nome = t.Nome.ToString();
            ViewBag.descricao = t.Descricao.ToString();
            ViewBag.status = t.Status.ToString();
            ViewBag.data = t.Data.ToString();

            //-- visualiza os detalhes da tarefa--//
            return View();
        }

        /**
         * Autor: Lucas de Mello
         * Excluir tarefa selecionada
         * **/
        public ActionResult excluir(int id) {
            conn = null;
            bool fez = false;
            try {
                conn = ConexaoAccess.getConexaoAccess();

                fez = new TarefaDAO().excluirTarefaPorId(id);

            } catch (Exception e) {
                Console.WriteLine(e.Message);

            } finally {
                if (fez) {
                    Session["sucesso"] = "Atenção! A tarefa selecionada foi excluída com sucesso.";

                } else {
                    Session["sucesso"] = "Atenção! Não foi possível excluir a tarefa selecionada, operação inválida";
                }

                ConexaoAccess.getFecharConexao(conn);

            }
            return RedirectToAction("index");
        }

        /**
         * Autor: Lucas de Mello
         * Metodo responsavel para gerenciar as edicoes das tarefas
         * **/
        public ActionResult editar(int id) {
            //-- definindo o titulo e o subtitulo da pagina --//
            ViewBag.titulo = "EDITAR DADOS DA TAREFA";
            ViewBag.subTitulo = "Atenção! Para alterar os dados, preencha os campos abaixo:";            

            //-- se a tarefa foi preenchida realiza o cadastro --//            
            conn = null;
            Tarefa t = null;
            try
            {
                conn = ConexaoAccess.getConexaoAccess();

                //-- recuperando a tarefa selecionada por id --//
                t = new TarefaDAO().recuperarTarefaPorId(id);                

                //-- montando a lista de opcoes dos status  --//
                var opcoesStatus = new OpcoesStatus();
                var listarOpcoes = opcoesStatus.ListaOpcoes();
                ViewBag.listaStatus = new SelectList(listarOpcoes, "Status", "Nome"); //esta informação com o nome dos campos

            }
            catch (Exception e)
            {                
                Console.WriteLine(e.Message);
            }
            finally
            {
                ConexaoAccess.getFecharConexao(conn);
            }

            return View(t);

        }

        [HttpPost]
        public ActionResult editar(int id, Tarefa t)
        {            

            //-- se a tarefa foi preenchida realiza a alteracao --//            
            conn = null;
            bool fez = false;

            try
            {
                t.id = id;

                conn = ConexaoAccess.getConexaoAccess();
                fez = new TarefaDAO().alterarDadosTarefa(t);               

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fez)
                {
                    Session["sucesso"] = "Atenção! Os dados da tarefa foram alterados com sucesso.";
                }
                else{
                    Session["sucesso"] = "Atenção! Não foi possível realizar a alteração dos dados da tarefa, operação inválida.";
                }


                ConexaoAccess.getFecharConexao(conn);
            }

            return RedirectToAction("index");

        }

        /**
         * Autor: Lucas de Mello
         * Localizar tarefas por parte do nome
         * **/
         [HttpPost]
        public string Pesquisar(string search) {

            return "nome a ser localizado = " + search;
        }



    }
}
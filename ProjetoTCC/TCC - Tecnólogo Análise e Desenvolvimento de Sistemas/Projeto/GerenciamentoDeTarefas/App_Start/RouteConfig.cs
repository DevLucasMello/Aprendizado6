using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GerenciamentoDeTarefas
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //-- criando uma rota amigavel --//
            routes.MapRoute(
              "contato",
              "contato",
              new { controller = "Home", action = "contact"}
            );

            //-- criando uma rota amigavel --//
            routes.MapRoute(
              "inserirtarefas",
              "inserirtarefas",
              new { controller = "Home", action = "cadastrartarefas" }
            );

            //-- criando uma rota com parametros  --//
            
            //new { controller = "Home", action = "contact" }
            //);

            //-- essa rota padrao tem que estar no final --//
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

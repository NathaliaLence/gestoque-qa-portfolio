using Gestoque.AutoMapper;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Gestoque
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            // O banco é criado pelos scripts da pasta database/; o EF não deve tentar criá-lo
            System.Data.Entity.Database.SetInitializer<Gestoque.Models.ContextoBD>(null);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpRequestValidationException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.ContentType = "application/json";
                Response.Write("{ \"Resultado\":\"AVISO\",\"Mensagens\":[\"Somente texto sem caracteres especiais pode ser enviado.\"],\"IdSalvo\":\"\"}");
                Response.End();
            }
            else if (ex is HttpAntiForgeryException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.End();
                // gravar LOG
            }
        }

        //a cada aquisição vrefica a autenticação
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //obtive o cookie
            var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            //se o usuario estiver autenticado
            if (cookie != null && cookie.Value != string.Empty)
            {
                //descriptografar
                FormsAuthenticationTicket ticket;
                try
                {
                    ticket = FormsAuthentication.Decrypt(cookie.Value);
                }
                catch
                {
                    return;
                }

                var partes = ticket.UserData.Split('|');
                var id = Convert.ToInt32(partes[0]);
                //recupera a informação e se tiver ";" o usuario tem mais de um perfil
                var perfis = partes[1].Split(';');
                //se já houver usuario no contexto
                if (Context.User != null)
                {
                    Context.User = new AplicacaoPrincipal(Context.User.Identity, perfis, id);
                }
            }
        }
    }
}

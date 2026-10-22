using Gestoque.Models;
using Rotativa;
using System.Web.Mvc;

namespace Gestoque.Controllers.Relatorio
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class RelatorioRessuprimentoController : Controller
    {
        public ActionResult Filtro()
        {
            if (Request.HttpMethod == "GET")
            {
                return View("~/Views/Relatorio/FiltroRelatorioRessuprimento.cshtml");
            }
            else
            {
                var minimo = 0;
                int.TryParse(Request.Form.Get("minimo"), out minimo);
                if (minimo <= 0)
                {
                    ViewBag.Mensagem = "Informe a quantidade mínima.";
                    return View("~/Views/Relatorio/FiltroRelatorioRessuprimento.cshtml");
                }

                return RedirectToAction("Index", new { minimo });
            }
        }

        public ActionResult Index(int minimo)
        {
            if (minimo == 0)
            {
                return RedirectToAction("Filtro");
            }

            var estoque = ProdutoModel.RecuperarRelatRessuprimento(minimo);

            return new ViewAsPdf("~/Views/Relatorio/RelatorioRessuprimento.cshtml", estoque);
        }
    }
}
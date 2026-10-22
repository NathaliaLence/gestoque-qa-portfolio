using Gestoque.Models;
using Rotativa;
using System.Web.Mvc;

namespace Gestoque.Controllers.Relatorio
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]

    public class RelatorioPosicaoEstoqueController : Controller
    {
        public ActionResult Index()
        {
            var estoque = ProdutoModel.RecuperarRelatPosicaoEstoque();
            return new ViewAsPdf("~/Views/Relatorio/RelatorioPosicaoEstoque.cshtml", estoque);
        }
    }
}
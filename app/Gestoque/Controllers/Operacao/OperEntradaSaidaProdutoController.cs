using AutoMapper;
using Gestoque.Models;
using Gestoque.Models.Binder;
using Gestoque.Models.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Gestoque.Controllers.Operacao
{
    public abstract class OperEntradaSaidaProdutoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Produtos = Mapper.Map<List<ProdutoViewModel>>(ProdutoModel.RecuperarLista(somenteAtivos: true));

            return View();
        }

        protected abstract string SalvarPedido(EntradaSaidaProdutoViewModel dados);

        public JsonResult Salvar([ModelBinder(typeof(EntradaSaidaProdutoViewModelBinder))]EntradaSaidaProdutoViewModel dados)
        {
            var numPedido = SalvarPedido(dados);
            var ok = (numPedido != "");

            return Json(new { OK = ok, Numero = numPedido });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Gestoque.Models;
using Gestoque.Models.ViewModel;

namespace Gestoque.Controllers.Cadastro
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadUnidadeMedidaController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = Mapper.Map<List<UnidadeMedidaViewModel>>(UnidadeMedidaModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            var quant = UnidadeMedidaModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult UnidadeMedidaPagina(int pagina, int tamPag, string filtro)
        {
            var lista = Mapper.Map<List<UnidadeMedidaViewModel>>(UnidadeMedidaModel.RecuperarLista(pagina, tamPag, filtro));
            return Json(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarUnidadeMedida(int id)
        {
            var vm = Mapper.Map<UnidadeMedidaViewModel>(UnidadeMedidaModel.RecuperarPeloId(id));
            return Json(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirUnidadeMedida(int id)
        {
            var resultado = "OK";

            try
            {
                var x = UnidadeMedidaModel.ExcluirPeloId(id);
                if (x == true)
                {
                    resultado = "OK";

                }
                else
                {
                    resultado = "ERRO";

                }
            }
            catch (Exception ex)
            {
                resultado = "ERRO";
            }


            return Json(new { Resultado = resultado });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarUnidadeMedida(UnidadeMedidaViewModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var vm = Mapper.Map<UnidadeMedidaModel>(model);
                    var id = vm.Salvar();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }
    }
}
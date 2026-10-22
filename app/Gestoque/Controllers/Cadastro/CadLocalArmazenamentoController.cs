using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gestoque.Models;
using Gestoque.Models.ViewModel;

namespace Gestoque.Controllers.Cadastro
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadLocalArmazenamentoController : Controller
    {
        //constante de quantidade linhas por pag
        private const int _quantMaxLinhasPorPagina = 5;

        //paginação
        [Authorize]
        public ActionResult Index()
        {
            //filtro
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = Mapper.Map<List<LocalArmazenamentoViewModel>>(LocalArmazenamentoModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            var quant = LocalArmazenamentoModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] //recupera lista com num de paginas
        public JsonResult LocalArmazenamentoPagina(int pagina, int tamPag, string filtro)
        {
            var lista = Mapper.Map<List<LocalArmazenamentoViewModel>>(LocalArmazenamentoModel.RecuperarLista(pagina, tamPag, filtro));
            return Json(lista);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] //valida o token
        public JsonResult RecuperarLocalArmazenamento(int id)
        {
            var vm = Mapper.Map<LocalArmazenamentoViewModel>(LocalArmazenamentoModel.RecuperarPeloId(id));
            return Json(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirLocalArmazenamento(int id)
        {
            var resultado = "OK";

            try
            {
                var x = LocalArmazenamentoModel.ExcluirPeloId(id);
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
        public JsonResult SalvarLocalArmazenamento(LocalArmazenamentoViewModel model)
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
                    var vm = Mapper.Map<LocalArmazenamentoModel>(model);
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
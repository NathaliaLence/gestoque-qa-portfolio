using AutoMapper;
using Gestoque.Models;
using Gestoque.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestoque.Controllers.Cadastro
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadProdutoController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = Mapper.Map<List<ProdutoViewModel>>(ProdutoModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            var quant = ProdutoModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;
            ViewBag.UnidadesMedida = Mapper.Map<List<UnidadeMedidaViewModel>>(UnidadeMedidaModel.RecuperarLista(1, 9999));
            ViewBag.Grupos = Mapper.Map<List<GrupoProdutoViewModel>>(GrupoProdutoModel.RecuperarLista(1, 9999));
            ViewBag.Marcas = Mapper.Map<List<MarcaProdutoViewModel>>(MarcaProdutoModel.RecuperarLista(1, 9999));
            ViewBag.Fornecedores = Mapper.Map<List<FornecedorViewModel>>(FornecedorModel.RecuperarLista(1, 9999));
            ViewBag.LocaisArmazenamento = Mapper.Map<List<LocalArmazenamentoViewModel>>(LocalArmazenamentoModel.RecuperarLista(1, 9999));

            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProdutoPagina(int pagina, int tamPag, string filtro)
        {
            var lista = Mapper.Map<List<ProdutoViewModel>>(ProdutoModel.RecuperarLista(pagina, tamPag, filtro));
            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarProduto(int id)
        {
            var vm = Mapper.Map<ProdutoViewModel>(ProdutoModel.RecuperarPeloId(id));
            return Json(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarQuantidadeEstoqueProduto(int id)
        {
            var model = ProdutoModel.RecuperarPeloId(id);
            if (model != null)
            {
                return Json(new { OK = true, Result = model.QuantEstoque });
            }
            else
            {
                return Json(new { OK = false });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirProduto(int id)
        {
            var resultado = "OK";

            try
            {
                var x = ProdutoModel.ExcluirPeloId(id);
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
        [ValidateAntiForgeryToken]
        public JsonResult SalvarProduto()
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            var model = new ProdutoModel()
            {
                Id = Int32.Parse(Request.Form["Id"]),
                Codigo = Request.Form["Codigo"],
                Nome = Request.Form["Nome"],
                PrecoCusto = Decimal.Parse(Request.Form["PrecoCusto"]),
                PrecoVenda = Decimal.Parse(Request.Form["PrecoVenda"]),
                QuantEstoque = Int32.Parse(Request.Form["QuantEstoque"]),
                IdUnidadeMedida = Int32.Parse(Request.Form["IdUnidadeMedida"]),
                IdGrupo = Int32.Parse(Request.Form["IdGrupo"]),
                IdMarca = Int32.Parse(Request.Form["IdMarca"]),
                IdFornecedor = Int32.Parse(Request.Form["IdFornecedor"]),
                IdLocalArmazenamento = Int32.Parse(Request.Form["IdLocalArmazenamento"]),
                Ativo = (Request.Form["Ativo"] == "true")
            };

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = model.Salvar();
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
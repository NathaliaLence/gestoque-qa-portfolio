using AutoMapper;
using Gestoque.Models;
using Gestoque.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Gestoque.Controllers.Cadastro
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadFornecedorController : Controller
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

            var lista = Mapper.Map<List<FornecedorViewModel>>(FornecedorModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            var quant = FornecedorModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] //recupera lista com num de paginas
        public JsonResult FornecedorPagina(int pagina, int tamPag, string filtro)
        {
            var lista = Mapper.Map<List<FornecedorViewModel>>(FornecedorModel.RecuperarLista(pagina, tamPag, filtro));
            return Json(lista);

        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] //valida o token
        public JsonResult RecuperarFornecedor(int id)
        {
            var vm = Mapper.Map<FornecedorViewModel>(FornecedorModel.RecuperarPeloId(id));
            return Json(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirFornecedor(int id)
        {
           var resultado = "OK";
            
                 try
                 {
                     var x = FornecedorModel.ExcluirPeloId(id);
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
             

             return Json(new { Resultado = resultado});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarFornecedor(FornecedorViewModel model)
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
                    var vm = Mapper.Map<FornecedorModel>(model) ;
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
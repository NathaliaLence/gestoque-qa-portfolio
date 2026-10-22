using Gestoque.Models;
using System.Web.Mvc;

namespace Gestoque.Controllers.Operacao
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class OperEntradaProdutoController : OperEntradaSaidaProdutoController
    {
        protected override string SalvarPedido(EntradaSaidaProdutoViewModel dados)
        {
            return ProdutoModel.SalvarPedidoEntrada(dados.Data, dados.Produtos);
        }
    }
}
using System;

namespace Gestoque.Models.EntityModel
{
    public class SaidaProdutoModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public int IdProduto { get; set; }
        public virtual ProdutoModel Produto { get; set; }
    }
}
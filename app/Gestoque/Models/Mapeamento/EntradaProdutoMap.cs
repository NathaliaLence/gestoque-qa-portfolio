using Gestoque.Models.EntityModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gestoque.Models.Mapeamento
{
    public class EntradaProdutoMap : EntityTypeConfiguration<EntradaProdutoModel>
    {
        public EntradaProdutoMap()
        {
            ToTable("entrada_produto");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
            Property(x => x.Data).HasColumnName("data").IsRequired();
            Property(x => x.Quantidade).HasColumnName("quant").IsRequired();

            Property(x => x.IdProduto).HasColumnName("id_produto").IsRequired(); //associação do produto
            HasRequired(x => x.Produto).WithMany().HasForeignKey(x => x.IdProduto).WillCascadeOnDelete(false); //relação de chave FK
        }
    }
}
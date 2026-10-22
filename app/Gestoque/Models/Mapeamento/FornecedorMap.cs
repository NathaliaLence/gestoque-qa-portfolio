using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gestoque.Models.Mapeamento
{
    public class FornecedorMap : EntityTypeConfiguration<FornecedorModel>
    {
        public FornecedorMap()
        {
            ToTable("fornecedor");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(60).IsRequired();
            Property(x => x.RazaoSocial).HasColumnName("razao_social").HasMaxLength(100).IsOptional();
            Property(x => x.NumDocumento).HasColumnName("num_documento").HasMaxLength(20).IsOptional();
            Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(20).IsRequired();
            Property(x => x.Contato).HasColumnName("contato").HasMaxLength(60).IsRequired();
            Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(100).IsRequired();
            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(15).IsRequired();
            Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(100).IsOptional();
            Property(x => x.Cep).HasColumnName("cep").HasMaxLength(10).IsOptional();
            Property(x => x.Estado).HasColumnName("estado").HasMaxLength(60).IsRequired();
            Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(60).IsRequired();
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        }
    }
}
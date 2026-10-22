using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Gestoque.Models.Mapeamento
{
    public class UsuarioMap : EntityTypeConfiguration<UsuarioModel> //classe base para mapeamento
    {
        public UsuarioMap()
        {
            ToTable("usuario"); //nome tabela

            HasKey(x => x.Id); //chave primaria
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); //campo id é um autoincremento

            //especificando os parametros
            Property(x => x.Login).HasColumnName("login").HasMaxLength(50).IsRequired();
            Property(x => x.Senha).HasColumnName("senha").HasMaxLength(32).IsRequired();
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();
            Property(x => x.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
        }
    }
}
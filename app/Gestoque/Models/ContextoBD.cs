using Gestoque.Models.EntityModel;
using Gestoque.Models.Mapeamento;
using System.Data.Entity;

namespace Gestoque.Models
{
    public class ContextoBD : DbContext //classe de contexto do BD
    {
        public ContextoBD() : base("name=principal") //string de conexão para o banco de dados
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //mapeamento do EntityModel(models)
        {
            modelBuilder.Configurations.Add(new EntradaProdutoMap());
            modelBuilder.Configurations.Add(new FornecedorMap());
            modelBuilder.Configurations.Add(new GrupoProdutoMap());
            modelBuilder.Configurations.Add(new InventarioEstoqueMap());
            modelBuilder.Configurations.Add(new LocalArmazenamentoMap());
            modelBuilder.Configurations.Add(new MarcaProdutoMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new SaidaProdutoMap());
            modelBuilder.Configurations.Add(new UnidadeMedidaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
        }


        public DbSet<EntradaProdutoModel> EntradasProdutos { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<GrupoProdutoModel> GruposProdutos { get; set; }
        public DbSet<InventarioEstoqueModel> InventariosEstoque { get; set; }
        public DbSet<LocalArmazenamentoModel> LocaisArmazenamentos { get; set; }
        public DbSet<MarcaProdutoModel> MarcasProdutos { get; set; }
        public DbSet<PerfilModel> PerfisUsuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<SaidaProdutoModel> SaidasProdutos { get; set; }
        public DbSet<UnidadeMedidaModel> UnidadesMedida { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}

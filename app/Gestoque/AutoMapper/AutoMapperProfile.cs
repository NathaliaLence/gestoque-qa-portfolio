using AutoMapper;
using Gestoque.Models;
using Gestoque.Models.ViewModel;

//mapeamento de EntityModel para ViewModel
namespace Gestoque.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FornecedorViewModel, FornecedorModel>();
            CreateMap<GrupoProdutoViewModel, GrupoProdutoModel>();
            CreateMap<LocalArmazenamentoViewModel, LocalArmazenamentoModel>();
            CreateMap<MarcaProdutoViewModel, MarcaProdutoModel>();
            CreateMap<PerfilViewModel, PerfilModel>();
            CreateMap<ProdutoViewModel, ProdutoModel>();
            CreateMap<UnidadeMedidaViewModel, UnidadeMedidaModel>();
            CreateMap<UsuarioViewModel, UsuarioModel>();
        }
    }
}
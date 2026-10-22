using AutoMapper;

namespace Gestoque.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //inicializa o automapper 
            Mapper.Initialize(x =>
            {
                //adicionando o profile que preciso
                x.AddProfile<AutoMapperProfile>();
            });
        }
    }
}
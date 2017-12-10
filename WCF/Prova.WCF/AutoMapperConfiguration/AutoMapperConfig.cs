using AutoMapper;

namespace Prova.WCF.AutoMapperConfiguration
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {                
                x.AddProfile<DomainToDataContractsMappingProfile>();                
                x.AddProfile<DataContractsToDomainMappingProfile>();                
            });

        }
    }
}
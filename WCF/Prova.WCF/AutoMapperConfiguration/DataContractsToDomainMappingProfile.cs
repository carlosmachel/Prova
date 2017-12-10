using AutoMapper;

namespace Prova.WCF.AutoMapperConfiguration
{
    public class DataContractsToDomainMappingProfile : Profile
    {

        public DataContractsToDomainMappingProfile()
        {
            CreateMap<Domain.Entities.Contato, DataContracts.Contato>();
            CreateMap<Domain.Entities.Usuario, DataContracts.Usuario>();
        }
    }
}
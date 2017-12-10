using AutoMapper;

namespace Prova.WCF.AutoMapperConfiguration
{
    public class DomainToDataContractsMappingProfile : Profile
    {
        public DomainToDataContractsMappingProfile()
        {
            CreateMap<DataContracts.Contato, Domain.Entities.Contato>();
            CreateMap<DataContracts.Usuario, Domain.Entities.Usuario>().Ignore(u=>u.Contatos);
        }
    }
}
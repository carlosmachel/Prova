using Prova.WCF.DataContracts;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Prova.WCF
{
    [ServiceContract]
    public interface IContatoService
    {
        [OperationContract]
        int CadastrarContato(Contato contato);

        [OperationContract]
        bool AlterarContato(Contato contato);

        [OperationContract]
        bool RemoverContato(int contatoId);

        [OperationContract]
        IEnumerable<Contato> BuscarContatos(int usuarioId);

    }   
}

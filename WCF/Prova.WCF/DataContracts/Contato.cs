using System.Runtime.Serialization;

namespace Prova.WCF.DataContracts
{
    [DataContract]
    public class Contato : IDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Telefone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int UsuarioId { get; set; }        
    }

    public class CadastroContato: IDataContract
    {
        [DataMember]
        public long Id { get; set; }
    }
}
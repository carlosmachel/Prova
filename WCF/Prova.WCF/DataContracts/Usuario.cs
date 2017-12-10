using System.Runtime.Serialization;

namespace Prova.WCF.DataContracts
{
    [DataContract]
    public class Usuario : IDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Senha { get; set; }

        [DataMember]
        public string Codigo { get; set; }     
    }

    public class RetornoCadastroUsuario : IDataContract
    {
        [DataMember]
        public int Id { get; set; }
    }
}
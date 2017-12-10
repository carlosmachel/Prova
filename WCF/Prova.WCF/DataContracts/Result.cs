using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Prova.WCF.DataContracts
{
    [DataContract(Name = "ResultOfType{0}")]
    public class Result<T> where T : IDataContract                           
    {
        [DataMember]
        public bool Sucesso { get; set; }
        [DataMember]
        public string Mensagem { get; set; }
        [DataMember]
        public T Data { get; set; }
        
    }
}
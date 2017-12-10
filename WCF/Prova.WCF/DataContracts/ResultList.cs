using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Prova.WCF.DataContracts
{
    [DataContract]
    public class ResultList<T> where T : IEnumerable<IDataContract>
    {
        [DataMember]
        public bool Sucesso { get; set; }
        [DataMember]
        public string Mensagem { get; set; }
        [DataMember]
        public T Data { get; set; }

    }
}
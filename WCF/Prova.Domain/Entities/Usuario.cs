using System.Collections.Generic;

namespace Prova.Domain.Entities
{
    public class Usuario
    {        
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Codigo { get; set; }
        public virtual IEnumerable<Contato> Contatos { get; set; }
    }
}

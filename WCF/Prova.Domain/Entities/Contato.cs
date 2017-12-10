namespace Prova.Domain.Entities
{
    public class Contato
    {        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }        
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

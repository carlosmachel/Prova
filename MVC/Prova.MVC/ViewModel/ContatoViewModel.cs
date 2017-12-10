using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prova.MVC.ViewModel
{
    public class ContatoViewModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public long UsuarioId { get; set; }
    }
}
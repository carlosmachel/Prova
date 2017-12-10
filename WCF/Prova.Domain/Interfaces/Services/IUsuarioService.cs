using Prova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Domain.Servicos
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        bool VerificarEmailExiste(string email);

        Usuario ObterPorEmailSenha(string email, string senha);

        Usuario ObterPorEmail(string email);

        bool ValidarCodigo(int usuarioId, string codigo);
        Usuario AutenticarComIdSenha(int usuarioId, string senha);
    }
}

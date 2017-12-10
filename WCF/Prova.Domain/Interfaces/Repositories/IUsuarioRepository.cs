using Prova.Domain.Entities;

namespace Prova.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        bool VerificarEmailExiste(string email);
        Usuario ObterPorEmailSenha(string email, string senha);

        Usuario ObterPorEmail(string email);

        bool ValidarCodigo(int usuarioId, string codigo);

        Usuario AutenticarComIdSenha(int usuarioId, string senha);
    }
}

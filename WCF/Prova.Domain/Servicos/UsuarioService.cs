using Prova.Domain.Entities;
using Prova.Domain.Interfaces;

namespace Prova.Domain.Servicos
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
            : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool VerificarEmailExiste(string email)
        {
            return _usuarioRepository.VerificarEmailExiste(email);
        }

        public Usuario ObterPorEmailSenha(string email, string senha)
        {
            return _usuarioRepository.ObterPorEmailSenha(email, senha);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepository.ObterPorEmail(email);
        }

        public bool ValidarCodigo(int usuarioId, string codigo)
        {
            return _usuarioRepository.ValidarCodigo(usuarioId, codigo);
        }
     
        public Usuario AutenticarComIdSenha(int usuarioId, string senha)
        {
            return _usuarioRepository.AutenticarComIdSenha(usuarioId, senha);
        }
    }
}

using AutoMapper;
using Prova.WCF.DataContracts;
using System;

namespace Prova.WCF
{

    public class UsuarioService : IUsuarioService
    {
        private readonly Domain.Servicos.IUsuarioService usuarioService;
        private readonly Domain.Interfaces.IUsuarioRepository usuarioRepository;

        public UsuarioService()
        {
            usuarioRepository = new Infra.Data.Repositorios.UsuarioRepository();
            usuarioService = new Domain.Servicos.UsuarioService(usuarioRepository);
        }

        public Result<Usuario> Autenticar(string email, string senha)
        {
            try
            {
                var usuario = usuarioService.ObterPorEmailSenha(email, senha);

                if (usuario != null)
                {

                    return new Result<Usuario> { Sucesso = true, Data = Mapper.Map<Domain.Entities.Usuario, Usuario>(usuario) };
                }
                else
                {
                    return new Result<Usuario> { Sucesso = false, Mensagem = "Usuário/Senha incorreto. " };
                }

            }
            catch (Exception)
            {
                return new Result<Usuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public Result<RetornoCadastroUsuario> CadastrarUsuario(Usuario usuario)
        {
            try
            {
                if (!usuarioService.VerificarEmailExiste(usuario.Email))
                {
                    var entity = Mapper.Map<Usuario, Domain.Entities.Usuario>(usuario);

                    usuarioService.Add(entity);

                    return new Result<RetornoCadastroUsuario> { Sucesso = true, Data = new RetornoCadastroUsuario { Id = entity.Id } };
                }
                else
                {
                    return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "E-mail já cadastrado no sistema." };
                }
            }
            catch (Exception)
            {
                return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public Result<RetornoCadastroUsuario> AlterarNomeUsuario(string nome, int usuarioId)
        {
            try
            {
                var usuario = usuarioService.GetById(usuarioId);

                usuario.Nome = nome;

                usuarioService.Update(usuario);

                return new Result<RetornoCadastroUsuario> { Sucesso = true };
            }
            catch (Exception)
            {
                return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public Result<RetornoCadastroUsuario> AlterarSenha(string senhaAntiga, string senhaNova, int usuarioId)
        {
            try
            {
                var usuario = usuarioService.AutenticarComIdSenha(usuarioId, senhaAntiga);

                if(usuario != null)
                {
                    usuario.Senha = senhaNova;
                    usuarioService.Update(usuario);
                }
                else
                {
                    return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Senha incorreta" };
                }
                
                return new Result<RetornoCadastroUsuario> { Sucesso = true };
            }
            catch (Exception)
            {
                return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public Result<RetornoCadastroUsuario> ResetarSenha(string senha, string codigo, int usuarioId)
        {
            try
            {
                var usuario = usuarioService.GetById(usuarioId);

                if (usuario != null && usuario.Codigo == codigo)
                {
                    usuario.Senha = senha;
                    usuario.Codigo = null;

                    usuarioService.Update(usuario);

                    return new Result<RetornoCadastroUsuario> { Sucesso = true };
                }
                else
                {
                    return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Código/Usuário não encontrado." };
                }
            }
            catch (Exception)
            {
                return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public Result<RetornoCadastroUsuario> AdicionarCodigoRecuperacaoSenha(string codigo, int usuarioId)
        {
            try
            {
                var usuario = usuarioService.GetById(usuarioId);

                usuario.Codigo = codigo;

                usuarioService.Update(usuario);

                return new Result<RetornoCadastroUsuario> { Sucesso = true };
            }
            catch (Exception)
            {
                return new Result<RetornoCadastroUsuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public Result<Usuario> BuscarUsuarioPorEmail(string email)
        {
            try
            {
                var usuario = usuarioService.ObterPorEmail(email);

                if (usuario != null)
                {
                    return new Result<Usuario> { Sucesso = true, Data = Mapper.Map<Domain.Entities.Usuario, Usuario>(usuario) };
                }
                else
                {
                    return new Result<Usuario> { Sucesso = false, Mensagem = "Usuário não encontrado." };
                }

            }
            catch (Exception)
            {
                return new Result<Usuario> { Sucesso = false, Mensagem = "Algo inesperado aconteceu. Contato Suporte." };
            }
        }

        public bool RecuperarSenha(string codigo, int usuarioId)
        {
            try
            {
                var codigoExiste = usuarioService.ValidarCodigo(usuarioId, codigo);

                return codigoExiste;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarCodigo(string codigo, string email)
        {
            try
            {
                var usuario = usuarioService.ObterPorEmail(email);

                if(usuario != null && usuario.Codigo == codigo)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

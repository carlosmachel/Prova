using Prova.Domain.Entities;
using Prova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Infra.Data.Repositorios
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public bool VerificarEmailExiste(string email)
        {
            return db.Usuarios.Any(u => u.Email == email);
        }

        public Usuario ObterPorEmailSenha(string email, string senha)
        {
            return db.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
        
        public Usuario ObterPorEmail (string email)
        {
            return db.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public bool ValidarCodigo(int usuarioId, string codigo)
        {
            return db.Usuarios.Any(u => u.Id == usuarioId && u.Codigo == codigo);
        }

        public Usuario AutenticarComIdSenha(int usuarioId, string senha)
        {
            return db.Usuarios.FirstOrDefault(u => u.Id == usuarioId && u.Senha == senha);
        }
        
    }
}

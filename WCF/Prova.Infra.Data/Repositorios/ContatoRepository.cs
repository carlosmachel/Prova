using Prova.Domain.Entities;
using Prova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Infra.Data.Repositorios
{
    public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
    {
        public IEnumerable<Contato> ObterContatosPorUsuario(int usuarioId)
        {
            return db.Contatos.Where(c => c.UsuarioId == usuarioId);
        }

        public Usuario ObterUsuario(int usuarioId)
        {
            return db.Usuarios.FirstOrDefault(x => x.Id == usuarioId);
        }
    }
}

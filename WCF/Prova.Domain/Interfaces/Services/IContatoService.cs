using Prova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Domain.Servicos
{
    public interface IContatoService : IServiceBase<Contato>
    {
        IEnumerable<Contato> ObterContatosPorUsuario(int usuarioId);

        Usuario ObterUsuario(int usuarioId);
    }
}

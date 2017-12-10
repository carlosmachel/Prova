using Prova.Domain.Entities;
using Prova.Domain.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace Prova.Domain.Servicos
{
    public class ContatoService : ServiceBase<Contato>, IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
            : base(contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }   
        
        public IEnumerable<Contato> ObterContatosPorUsuario(int usuarioId)
        {
            return _contatoRepository.ObterContatosPorUsuario(usuarioId);
        }     

        public Usuario ObterUsuario(int usuarioId)
        {
            return _contatoRepository.ObterUsuario(usuarioId);
        }
    }
}

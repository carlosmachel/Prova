using AutoMapper;
using Prova.WCF.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prova.WCF
{
    public class ContatoService : IContatoService
    {
        private readonly Domain.Servicos.IContatoService contatoService;
        private readonly Domain.Interfaces.IContatoRepository contatoRepository;
        
        public ContatoService()
        {
            contatoRepository = new Infra.Data.Repositorios.ContatoRepository();
            contatoService = new Domain.Servicos.ContatoService(contatoRepository);
        }

        public int CadastrarContato(Contato contato)
        {
            try
            {
                var entity = Mapper.Map<Contato, Domain.Entities.Contato>(contato);
                entity.Usuario = contatoService.ObterUsuario(contato.UsuarioId);
                contatoService.Add(entity);                                
                
                return entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AlterarContato(Contato contato)
        {
            try
            {
                var alterado = contatoService.GetById(contato.Id);                
                alterado.Email = contato.Email;
                alterado.Nome = contato.Nome;
                alterado.Telefone = contato.Telefone;

                contatoService.Update(alterado);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoverContato(int contatoId)
        {
            try
            {
                var contato = contatoService.GetById(contatoId);


                contatoService.Remove(contato);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Contato> BuscarContatos(int usuarioId)
        {            
            var contatos = Mapper.Map<IEnumerable<Domain.Entities.Contato>, IEnumerable<Contato>>(contatoService.ObterContatosPorUsuario(usuarioId));
            return contatos;
        }
    }
}

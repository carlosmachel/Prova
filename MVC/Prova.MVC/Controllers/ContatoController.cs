using Prova.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Prova.MVC.Controllers
{
    [Authorize]
    public class ContatoController : Controller
    {
        private WCF.ContatoService contatoCliente;
        
        public ContatoController()
        {
            contatoCliente = new WCF.ContatoService();
        }

        // GET: Contato
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioId = 0;

            int.TryParse(claimsIdentity.FindFirst("Id").Value, out usuarioId);

            return View(contatoCliente.BuscarContatos(usuarioId).Select(x=> new ContatoViewModel { Email = x.Email, Id = x.Id, Nome = x.Nome, Telefone = x.Telefone }));
        }

        [HttpPost]
        public ActionResult Criar(ContatoViewModel contato)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                int usuarioId = 0;

                int.TryParse(claimsIdentity.FindFirst("Id").Value, out usuarioId);

                var contatoMember = new WCF.DataContracts.Contato
                {
                    Email = contato.Email,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    UsuarioId = usuarioId
                };

                var contatoId = contatoCliente.CadastrarContato(contatoMember);

                return Json(contatoId);
            }

            return View(contato);
        }

        [HttpDelete]
        public ActionResult Remover(int id)
        {
            if (contatoCliente.RemoverContato(id))
            {
                return Json(new { Sucesso = true });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Editar(ContatoViewModel contato)
        {
            if (ModelState.IsValid)
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                int usuarioId = 0;

                int.TryParse(claimsIdentity.FindFirst("Id").Value, out usuarioId);

                var contatoMember = new WCF.DataContracts.Contato
                {
                    Id = contato.Id,
                    Email = contato.Email,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    UsuarioId = usuarioId
                };

                if (contatoCliente.AlterarContato(contatoMember))
                {
                    return Json(contato.Id);
                }
            }

            return View(contato);
        }

    }
}
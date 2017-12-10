using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Prova.MVC.Services
{
    public class AutenticacaoService
    {
        public static void Autenticar(string nome, string email, long usuarioId, HttpRequestBase request)
        {
            var identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id", usuarioId.ToString())

                }, "ApplicationCookie");

            var ctx = request.GetOwinContext();

            var authManager = ctx.Authentication;

            authManager.SignIn(identity);
        }   
        
        public static int BuscarIdUsuario(ClaimsIdentity identity)
        {            
            var usuarioId = 0;

            int.TryParse(identity.FindFirst("Id").Value, out usuarioId);

            return usuarioId;
        }   

        public static string BuscarEmail(ClaimsIdentity identity)
        {
            return identity.FindFirst(ClaimTypes.Email).Value;
        }
    }
}
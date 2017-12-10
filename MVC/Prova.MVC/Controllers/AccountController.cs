using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Prova.MVC.Models;
using Prova.MVC.CustomAttributes;
using System.Net;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using Prova.MVC.Services;
using System.Text;

namespace Prova.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private WCF.UsuarioService usuarioClient;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController()
        {
            usuarioClient = new WCF.UsuarioService();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RetornarComErro(ModelState);
            }


            var result = usuarioClient.Autenticar(model.Email, model.SenhaHash);

            if (result.Sucesso)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, result.Data.Nome),
                    new Claim(ClaimTypes.Email, result.Data.Email),
                    new Claim("Id", result.Data.Id.ToString())

                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Home");
                return Json(new { Url = redirectUrl });
            }
            else
            {

                ModelState.AddModelError("", result.Mensagem);

                return RetornarComErro(ModelState);
            }
        }

        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateCookieAntiForgeryToken]
        public ActionResult Registrar(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new WCF.DataContracts.Usuario
                {
                    Nome = model.Nome,
                    Senha = model.SenhaHash,
                    Email = model.Email
                };

                var result = usuarioClient.CadastrarUsuario(usuario);

                if (result.Sucesso)
                {
                    AutenticacaoService.Autenticar(usuario.Nome, usuario.Email, result.Data.Id, Request);

                    return RetornarJsonRedirect("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.Mensagem);
                    return RetornarComErro(ModelState);
                }
            }
            else
            {
                return RetornarComErro(ModelState);
            }
        }


        public ActionResult AlterarDados()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var model = new AlterarDadosViewModel(claimsIdentity.Name, claimsIdentity.FindFirst(ClaimTypes.Email).Value);

            return View(model);
        }

        [HttpPost]
        [ValidateCookieAntiForgeryToken]
        public ActionResult AlterarDados(AlterarDadosViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = AutenticacaoService.BuscarIdUsuario(User.Identity as ClaimsIdentity);

                var result = usuarioClient.AlterarNomeUsuario(model.Nome, usuarioId);

                if (result.Sucesso)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    AutenticacaoService.Autenticar(model.Nome, AutenticacaoService.BuscarEmail(User.Identity as ClaimsIdentity), usuarioId, Request);

                    return Json(new { Sucesso = true });
                }
                else
                {
                    ModelState.AddModelError("", result.Mensagem);
                    return RetornarComErro(ModelState);
                }
            }
            else
            {
                return RetornarComErro(ModelState);
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetarSenha(string code)
        {
            try
            {
                var bytes = Convert.FromBase64String(code);

                var decBase64 = Encoding.UTF8.GetString(Aes256.descriptografar("Prova", bytes));



                var model = new ResetarSenhaViewModel
                {
                    Code = code,
                    Email = decBase64.Split(',')[0]
                };

                if(usuarioClient.ValidarCodigo(code, model.Email))
                {
                    return View(model);
                }
                else
                {
                    return View("Error");
                }     
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateCookieAntiForgeryToken]
        public async Task<ActionResult> ResetarSenha(ResetarSenhaViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return RetornarComErro(ModelState);
            }

            var result = usuarioClient.BuscarUsuarioPorEmail(model.Email);
            if (result.Sucesso)
            {
                usuarioClient.ResetarSenha(model.SenhaHash, model.Code, result.Data.Id);            
            }

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("ResetarSenhaConfirmacao", "Account");
            return Json(new { Url = redirectUrl });            
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateCookieAntiForgeryToken]
        public async Task<ActionResult> EsqueciSenha(EsqueciSenhaViewModel model)
        {
            if (ModelState.IsValid && GoogleRecaptchaValidate.Validate(model.Captcha))
            {
                var result = usuarioClient.BuscarUsuarioPorEmail(model.Email);

                if (result.Sucesso)
                {

                    var entrada = model.Email + "," + Guid.NewGuid().ToString();

                    var senha = "Prova";

                    var cripto = Aes256.criptografar(senha, entrada);
                    var cripto64 = Convert.ToBase64String(cripto);

                    var resultCodigo = usuarioClient.AdicionarCodigoRecuperacaoSenha(cripto64, result.Data.Id);

                    if (resultCodigo.Sucesso)
                    {
                        var callbackUrl = Url.Action("ResetarSenha", "Account", new { userId = result.Data.Id, code = cripto64 }, protocol: Request.Url.Scheme);

                        //new SendEmailService("ADICIONE_SEU_EMAIL", "ADICIONE_SUA_SENHA").Send(model.Email, "Resetar Senha - Prova", "Por favor clique no link para resetar sua senha. " + callbackUrl);
                    }
                }

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("EsqueciSenhaConfirmacao", "Account");
                return Json(new { Url = redirectUrl });
            }

            return RetornarComErro(ModelState);
        }

        [AllowAnonymous]
        public ActionResult ResetarSenhaConfirmacao()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult EsqueciSenhaConfirmacao()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateCookieAntiForgeryToken]
        public ActionResult AlterarSenha(AlterarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                int usuarioId = 0;

                int.TryParse(claimsIdentity.FindFirst("Id").Value, out usuarioId);

                var result = usuarioClient.AlterarSenha(model.SenhaAntigaHash, model.SenhaNovaHash, usuarioId);

                if (result.Sucesso)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    var redirectUrl = new UrlHelper(Request.RequestContext).Action("ConfirmacaoAlteracaoSenha", "Account");
                    return Json(new { Url = redirectUrl });
                }
                else
                {
                    ModelState.AddModelError("", result.Mensagem);
                    return RetornarComErro(ModelState);                    
                }
            }
            else
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return new JsonResult { Data = new { codes = ModelState.SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage) } };
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmacaoAlteracaoSenha()
        {
            return View();
        }

        #region Helpers              
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private JsonResult RetornarComErro(ModelStateDictionary modelState)
        {
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult { Data = new { codes = modelState.SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage) } };

        }

        private JsonResult RetornarInternalServerError()
        {
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new JsonResult { Data = new { codes = new List<string> { "Algo inesperado aconteceu contate o suporte" } } };
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action(actionName: "Index", controllerName: "Home");
            }

            return returnUrl;
        }

        private JsonResult RetornarJsonRedirect(string actionName, string controllerName)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action(actionName, controllerName);
            return Json(new { Url = redirectUrl });
        }
        #endregion
    }
}
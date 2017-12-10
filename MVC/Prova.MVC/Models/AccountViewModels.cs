using Prova.Infra.CrossCutting.Helpers;
using Prova.MVC.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prova.MVC.Models
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        internal string SenhaHash
        {
            get
            {
                return SecurePasswordHasher.Hash(Senha);
            }
        }
    }

    public class RegistroViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter ao menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Senha", ErrorMessage = "A senha e sua confirmação devem ser iguais.")]
        public string ConfirmacaoSenha { get; set; }

        internal string SenhaHash
        {
            get
            {
                return SecurePasswordHasher.Hash(this.Senha);
            }        
        }
    }

    public class AlterarSenhaViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Antiga")]
        public string SenhaAntiga { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Nova")]
        public string SenhaNova { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("SenhaNova", ErrorMessage = "A senha e sua confirmação não são iguais.")]
        public string ConfirmacaoSenha { get; set; }

        internal string SenhaAntigaHash
        {
            get
            {
                return SecurePasswordHasher.Hash(SenhaAntiga);
            }
        }

        internal string SenhaNovaHash
        {
            get
            {
                return SecurePasswordHasher.Hash(SenhaNova);
            }
        }
    }

    public class AlterarDadosViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter ao menos {2} caracteres.", MinimumLength = 3)]        
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public string Email { get; }        

        public AlterarDadosViewModel()
        {

        }

        public AlterarDadosViewModel(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }

    public class ResetarSenhaViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Senha", ErrorMessage = "A senha e sua confirmação devem ser iguais.")]
        public string ConfirmacaoSenha { get; set; }

        [Required]
        public string Code { get; set; }

        internal string SenhaHash
        {
            get
            {
                return SecurePasswordHasher.Hash(this.Senha);
            }
        }
    }

    public class EsqueciSenhaViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Captcha { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Gestoque.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário")]
        public String Usuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }
    }
}
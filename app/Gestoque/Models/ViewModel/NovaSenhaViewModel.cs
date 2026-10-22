using System.ComponentModel.DataAnnotations;

namespace Gestoque.Models
{
    public class NovaSenhaViewModel
    {
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Informe confirmação correta da senha")]
        [DataType(DataType.Password)]
        public string ConfirmacaoSenha { get; set; }

        public int Usuario { get; set; }
    }
}
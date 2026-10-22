using System.ComponentModel.DataAnnotations;

namespace Gestoque.Models
{
    public class AlteracaoSenhaUsuarioViewModel
    {
        [Required(ErrorMessage = "Preencha a senha atual.")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Preencha a nova senha.")]
        [MinLength(3, ErrorMessage = "A nova senha deve ter no mínimo 3 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Preencha a confirmação da nova senha.")]
        [Compare("NovaSenha", ErrorMessage = "A senha e a confirmação devem ser iguais.")]
        public string ConfirmacaoNovaSenha { get; set; }
    }
}
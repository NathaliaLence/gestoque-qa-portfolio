using System.ComponentModel.DataAnnotations;

namespace Gestoque.Models
{
    public class EsqueciMinhaSenhaViewModel
    {
        [Required(ErrorMessage = "Informe o login")]
        public string Login { get; set; }
    }
}
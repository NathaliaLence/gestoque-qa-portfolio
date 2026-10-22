using System.ComponentModel.DataAnnotations;

namespace Gestoque.Models.ViewModel
{
    public class FornecedorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        [MaxLength(60, ErrorMessage = "O nome pode ter no máximo 60 caracteres.")]
        public string Nome { get; set; }

        [MaxLength(100, ErrorMessage = "A razão social pode ter no máximo 100 caracteres.")]
        public string RazaoSocial { get; set; }

        [MaxLength(20, ErrorMessage = "O número do documento pode ter no máximo 20 caracteres.")]
        public string NumDocumento { get; set; }

        [Required]
        public TipoPessoa Tipo { get; set; }

        [Required(ErrorMessage = "Preencha o telefone.")]
        [MaxLength(20, ErrorMessage = "O telefone deve ter 20 caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Preencha o contato.")]
        [MaxLength(60, ErrorMessage = "O contato deve ter no máximo 60 caracteres.")]
        public string Contato { get; set; }

        [Required(ErrorMessage = "Preencha o logradouro.")]
        [MaxLength(100, ErrorMessage = "O logradouro do endereço pode ter no máximo 100 caracteres.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Preencha o número do endereço.")]
        [MaxLength(15, ErrorMessage = "O número do endereço pode ter no máximo 15 caracteres.")]
        public string Numero { get; set; }

        [MaxLength(100, ErrorMessage = "O complemento do endereço pode ter no máximo 100 caracteres.")]
        public string Complemento { get; set; }

        [MaxLength(10, ErrorMessage = "O CEP do endereço pode ter no máximo 10 caracteres.")]
        public string Cep { get; set; }

        [MaxLength(60, ErrorMessage = "O estado deve ter no máximo 60 caracteres.")]
        public string Estado { get; set; }

        [MaxLength(60, ErrorMessage = "A cidade deve ter no máximo 60 caracteres.")]
        public string Cidade { get; set; }

        public bool Ativo { get; set; }
    }
}
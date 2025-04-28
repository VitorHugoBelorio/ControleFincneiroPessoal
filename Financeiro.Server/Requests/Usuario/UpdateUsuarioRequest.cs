using System.ComponentModel.DataAnnotations;

namespace Financeiro.Server.Requests.Usuario
{
    public class UpdateUsuarioRequest : Request
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }
}

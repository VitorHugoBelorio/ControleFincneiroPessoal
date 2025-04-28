using Financeiro.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace Financeiro.Server.Requests.Transacoes
{
    public class UpdateTransacaoRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Título inválido")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo inválido")]
        public ETransacaoType Type { get; set; } = ETransacaoType.Saque;

        [Required(ErrorMessage = "Valor inválido")]
        public decimal Quantia { get; set; }

        [Required(ErrorMessage = "Categoria inválida")]
        public long CategoriaId { get; set; }

        [Required(ErrorMessage = "Data inválida")]
        public DateTime? PagoOuRecebidoEm { get; set; }
    }
}

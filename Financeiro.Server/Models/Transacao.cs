namespace Financeiro.Server.Models
{
    public class Transacao
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal Quantia { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? PagoOuRecebidoEm { get; set; }

        public bool Type { get; set; }

        public long CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
    }
}

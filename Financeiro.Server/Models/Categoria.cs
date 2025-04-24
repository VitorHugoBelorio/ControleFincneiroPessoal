namespace Financeiro.Server.Models
{
    public class Categoria
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}

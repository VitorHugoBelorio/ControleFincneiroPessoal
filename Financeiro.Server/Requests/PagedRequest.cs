namespace Financeiro.Server.Requests
{
    public class PagedRequest : Request
    {
        public int PageSize { get; set; } = Configuracao.DefaultPageSize;
        public int PageNumber { get; set; } = Configuracao.DefaultPageNumber;
    }
}

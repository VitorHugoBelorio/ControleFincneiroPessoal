using Financeiro.Server.Configuracoes;
using Financeiro.Server.Resposta;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace Financeiro.Server.Resposta
{
    public class PagedResponse<TData> : Resposta<TData>
    {
        [JsonConstructor]
        public PagedResponse(
        TData? data,
        int totalCount,
            int currentPage = 1,
            int pageSize = Configuracao.DefaultPageSize)
            : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
        public PagedResponse(
            TData? data,
            int code = Configuracao.DefaultStatusCode,
            string? message = null)
            : base(data, code, message)
        {
        }

        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = Configuracao.DefaultPageSize;
        public int TotalCount { get; set; }
    }
}

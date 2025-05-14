using Financeiro.Server.Configuracoes;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace Financeiro.Server.Resposta
{
    public class Resposta<TData>
    {
        public Resposta(TData? data = default,
            int code = Configuracao.DefaultStatusCode,
            string? message = null)
        {
            Data = data;
            Code = code;
            Message = message;
        }

        public TData? Data { get; set; }

        public string? Message { get; set; }

        public int Code { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Code is >= 200 and <= 299;
    }
}

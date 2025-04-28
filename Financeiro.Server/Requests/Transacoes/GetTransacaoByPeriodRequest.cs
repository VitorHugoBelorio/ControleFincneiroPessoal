namespace Financeiro.Server.Requests.Transacoes
{
    public class GetTransacaoByPeriodRequest : PagedRequest
    {
        public DateTime? DataInicio { get; set; } // caso não informado será pego o primeiro dia do mês
        public DateTime? DataFinal { get; set; } // caso não informado será pego o último dia do mês
    }
}

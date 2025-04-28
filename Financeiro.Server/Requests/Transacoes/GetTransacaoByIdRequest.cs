namespace Financeiro.Server.Requests.Transacoes
{
    public class GetTransacaoByIdRequest : Request
    {
        public long Id { get; set; }
    }
}

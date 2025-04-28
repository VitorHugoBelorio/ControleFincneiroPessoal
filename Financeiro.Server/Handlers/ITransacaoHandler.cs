using Financeiro.Server.Models;
using Financeiro.Server.Requests.Transacoes;
using Financeiro.Server.Resposta;

namespace Financeiro.Server.Handlers
{
    public interface ITransacaoHandler
    {
        Task<Resposta<Transacao?>> CreateAsync(CreateTransacaoRequest request);
        Task<Resposta<Transacao?>> UpdateAsync(UpdateTransacaoRequest request);
        Task<Resposta<Transacao?>> DeleteAsync(DeleteTransacaoRequest request);
        Task<Resposta<Transacao?>> GetByIdAsync(GetTransacaoByIdRequest request);
        Task<PagedResponse<List<Transacao>?>> GetByPeriodAsync(GetTransacaoByPeriodRequest request);
    }
}

using Financeiro.Server.Models;
using Financeiro.Server.Requests.Categorias;
using Financeiro.Server.Requests.Usuario;
using Financeiro.Server.Resposta;

namespace Financeiro.Server.Handlers
{
    public interface IUsuarioHandler
    {
        Task<Resposta<Usuario?>> CreateAsync(CreateUsuarioRequest request);
        Task<Resposta<Usuario?>> UpdateAsync(UpdateUsuarioRequest request);
        Task<Resposta<Usuario?>> DeleteAsync(DeleteUsuarioRequest request);
    }
}

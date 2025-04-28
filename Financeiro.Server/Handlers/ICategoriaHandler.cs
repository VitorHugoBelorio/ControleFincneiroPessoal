using Financeiro.Server.Models;
using Financeiro.Server.Requests.Categorias;
using Financeiro.Server.Resposta;
using Financeiro.Server.Resposta;

namespace Financeiro.Server.Handlers
{
    public interface ICategoriaHandler
    {
        Task<Resposta<Categoria?>> CreateAsync(CreateCategoriaRequest request);
        Task<Resposta<Categoria?>> UpdateAsync(UpdateCategoriaRequest request);
        Task<Resposta<Categoria?>> DeleteAsync(DeleteCategoriaRequest request);
        Task<Resposta<Categoria?>> GetByIdAsync(GetCategoriaByIdRequest request);
        Task<PagedResponse<List<Categoria>?>> GetAllAsync(GetAllCategoriasRequest request);
    }
}

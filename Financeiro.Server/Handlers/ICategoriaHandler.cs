using Financeiro.Server.Models;
using Financeiro.Server.Response;
using Financeiro.Server.Resposta;

namespace Financeiro.Server.Handlers
{
    public interface ICategoriaHandler
    {
        Task<Resposta<Categoria?>> CreateAsync(CreateCategoryRequest request);
        Task<Resposta<Categoria?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Resposta<Categoria?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Resposta<Categoria?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Categoria>?>> GetAllAsync(GetAllCategoriesRequest request);
    }
}

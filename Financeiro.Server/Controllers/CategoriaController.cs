using Financeiro.Server.DataBase;
using Financeiro.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Financeiro.Server.Controllers
{
    [ApiController]
    [Route("v1/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {       
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");

                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value);


                var categoria = await _context.Categorias
                    .Where(c => c.UserId == usuarioId)
                    .ToListAsync();

                if (categoria == null || !categoria.Any())
                {
                    return NotFound(new { mensagem = "Nenhuma categoria encontrada." });
                }
                    
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao buscar categorias.",
                    erro = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategorias(long id) 
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }
                long usuarioId = long.Parse(userIdClaim.Value);
                var categoria = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Id == id && c.UserId == usuarioId);
                if (categoria == null)
                {
                    return NotFound(new { mensagem = "Categoria não encontrada." });
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao buscar categoria.",
                    erro = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] Categoria categoria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria.Titulo))
                {
                    return BadRequest(new { mensagem = "O título é obrigatório." });
                }

                // Pegar o ID do usuário logado a partir das Claims do token
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value); 

                // Atribuir o ID do usuário à categoria
                categoria.UserId = usuarioId;  

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensagem = "Categoria criada com sucesso.",
                    categoria
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao criar categoria.",
                    erro = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(long id, [FromBody] Categoria categoria) 
        {
            try
            {
                if (id != categoria.Id) // confere se o id da url é o mesmo do id do corpo da requisição
                {
                    return BadRequest(new { mensagem = "ID da URL não confere com o ID do corpo da requisição." });
                }

                var categoriaDb = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Id == categoria.Id); // necessário arrumar um jeito de passar o id da categoria que desjea ser atualizada
                
                if (categoriaDb == null)
                {
                    return NotFound(new { mensagem = "Categoria não encontrada." });

                }

                categoriaDb.Titulo = categoria.Titulo;
                categoriaDb.Descricao = categoria.Descricao;

                _context.Categorias.Update(categoriaDb);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    mensagem = "Categoria atualizada com sucesso.",
                    categoria = categoriaDb
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao atualizar categoria.",
                    erro = ex.Message
                });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(long id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.Id) // confere se o id da url é o mesmo do id do corpo da requisição
                {
                    return BadRequest(new { mensagem = "ID da URL não confere com o ID do corpo da requisição." });
                }

                var categoriaDb = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Id == categoria.Id); // necessário arrumar um jeito de passar o id da categoria que desjea ser atualizada

                if (categoriaDb == null)
                {
                    return NotFound(new { mensagem = "Categoria não encontrada." });

                }

                _context.Categorias.Remove(categoriaDb);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    mensagem = "Categoria excluida com sucesso.",
                    categoria = categoriaDb
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao excluir categoria.",
                    erro = ex.Message
                });
            }
        }
    }
}

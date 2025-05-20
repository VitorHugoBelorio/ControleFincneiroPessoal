using Financeiro.Server.DataBase;
using Financeiro.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Server.Controllers
{
    [ApiController]
    [Route("v1/transacao")]
    public class TransacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransacaoController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("categoria/{titulo}")]
        public async Task<IActionResult> GetAllTransacoes(string titulo)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");

                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value);


                var transacoes = await _context.Transacoes
                    .Include(t => t.Categoria)
                    .Where(t => t.UserId == usuarioId && t.Categoria.Titulo.ToLower() == titulo.ToLower())
                    .ToListAsync();

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao buscar transações.",
                    erro = ex.Message
                });
            }

        }

        [Authorize]
        [HttpGet("categoria/{titulo}/t")]
        public async Task<IActionResult> GetAllTransacoesTrue(string titulo)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");

                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value);


                var transacoes = await _context.Transacoes
                    .Include(t => t.Categoria)
                    .Where(t => t.UserId == usuarioId && t.Categoria.Titulo.ToLower() == titulo.ToLower() && t.Type == true)
                    .ToListAsync();

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao buscar transações.",
                    erro = ex.Message
                });
            }

        }

        [Authorize]
        [HttpGet("categoria/{titulo}/f")]
        public async Task<IActionResult> GetAllTransacoesFalse(string titulo)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");

                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value);


                var transacoes = await _context.Transacoes
                    .Include(t => t.Categoria)
                    .Where(t => t.UserId == usuarioId && t.Categoria.Titulo.ToLower() == titulo.ToLower() && t.Type == false)
                    .ToListAsync();

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao buscar transações.",
                    erro = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPost("categoria/id/{categoriaId}")]
        public async Task<IActionResult> CreateTransacaoPorId(long categoriaId, [FromBody] Transacao transacao)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == categoriaId);

            if (categoria == null)
                return NotFound(new { message = "Categoria não encontrada." });

            transacao.CategoriaId = categoriaId;
            transacao.CriadoEm = DateTime.Now;

            // Pega o ID do usuário autenticado do JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
                return Unauthorized();

            transacao.UserId = long.Parse(userIdClaim.Value);

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTransacaoPorId), new { id = transacao.Id }, transacao);
        }
    



        [Authorize]
        [HttpPut("categoria/{titulo}/{id}")]
        public async Task<IActionResult> AtualizarTransacao(string titulo, long id, [FromBody] Transacao transacaoAtualizada)
        {
            try
            {
                // Identifica o usuário autenticado
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value);

                // Busca a transação original
                var transacaoExistente = await _context.Transacoes
                    .FirstOrDefaultAsync(t => t.Id == id && t.UserId == usuarioId);

                if (transacaoExistente == null)
                {
                    return NotFound(new { mensagem = "Transação não encontrada." });
                }

                // Busca a nova categoria (se fornecida via rota)
                var categoria = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.UserId == usuarioId && c.Titulo.ToLower() == titulo.ToLower());

                if (categoria == null)
                {
                    return NotFound(new { mensagem = "Categoria não encontrada." });
                }

                // Validações
                if (string.IsNullOrWhiteSpace(transacaoAtualizada.Titulo))
                {
                    return BadRequest(new { mensagem = "O título da transação é obrigatório." });
                }

                if (transacaoAtualizada.Quantia <= 0)
                {
                    return BadRequest(new { mensagem = "A quantia da transação deve ser maior que zero." });
                }

                if (transacaoAtualizada.PagoOuRecebidoEm.HasValue &&
                    transacaoAtualizada.PagoOuRecebidoEm.Value > DateTime.Now)
                {
                    return BadRequest(new { mensagem = "A data de pagamento ou recebimento não pode estar no futuro." });
                }

                if (transacaoAtualizada.Type != true && transacaoAtualizada.Type != false)
                {
                    return BadRequest(new { mensagem = "O tipo da transação é inválido." });
                }

                // Atualiza os dados da transação
                transacaoExistente.Titulo = transacaoAtualizada.Titulo;
                transacaoExistente.Quantia = transacaoAtualizada.Quantia;
                transacaoExistente.PagoOuRecebidoEm = transacaoAtualizada.PagoOuRecebidoEm;
                transacaoExistente.Type = transacaoAtualizada.Type;
                transacaoExistente.CategoriaId = categoria.Id;
                transacaoExistente.Categoria = categoria;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensagem = "Transação atualizada com sucesso.",
                    transacao = transacaoExistente
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao atualizar transação.",
                    erro = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("por-categoria/{categoriaId}")]
        public async Task<IActionResult> GetByCategoria(long categoriaId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized(new { mensagem = "Usuário não identificado." });
                }

                long usuarioId = long.Parse(userIdClaim.Value);

                var transacoes = await _context.Transacoes
                    .Include(t => t.Categoria)
                    .Where(t => t.UserId == usuarioId && t.CategoriaId == categoriaId)
                    .ToListAsync();

                if (!transacoes.Any())
                {
                    return NotFound(new { mensagem = "Nenhuma transação encontrada para esta categoria." });
                }

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao buscar transações por categoria.",
                    erro = ex.Message
                });
            }
        }

    }
}

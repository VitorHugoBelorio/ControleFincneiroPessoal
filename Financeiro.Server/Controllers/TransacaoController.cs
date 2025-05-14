using Financeiro.Server.DataBase;
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
    }
}

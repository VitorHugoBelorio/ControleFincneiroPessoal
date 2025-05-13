using Financeiro.Server.DataBase;
using Financeiro.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Server.Controllers
{
    [ApiController]
    [Route("v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    return BadRequest(new { mensagem = "Email e senha são obrigatórios." });
                }

                // Consulta ao banco
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

                if (usuarioExistente == null)
                {
                    return Unauthorized(new { mensagem = "Credenciais inválidas." });
                }

                // Token falso (pode ser substituído por JWT futuramente)
                //var fakeToken = "tokenFake123";

                return Ok(new
                {
                    mensagem = "Login realizado com sucesso.",
                    usuario = new { usuarioExistente.Email },
                    //token = fakeToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao realizar login.",
                    erro = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    return BadRequest(new { mensagem = "Nome, email e senha são obrigatórios." });
                }
                // Verifica se o usuário já existe
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email);
                if (usuarioExistente != null)
                {
                    return Conflict(new { mensagem = "Usuário já existe." });
                }
                // Adiciona o novo usuário ao banco de dados
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CriarUsuario), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao criar usuário.",
                    erro = ex.Message
                });
            }


        }

        [HttpGet("perfil")]
        public async Task<IActionResult> GetUsuario()
        {
            try
            {
                // a implementar
            }
            catch (Exception ex)
            {

            }
        }
    }
}

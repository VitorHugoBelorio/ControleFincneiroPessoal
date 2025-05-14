using Financeiro.Server.Configuracoes;
using Financeiro.Server.DataBase;
using Financeiro.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Financeiro.Server.Controllers
{
    [ApiController]
    [Route("v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public UsuarioController(AppDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    return BadRequest(new { mensagem = "Email e senha são obrigatórios." });
                }

                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha);

                if (usuarioExistente == null)
                {
                    return Unauthorized(new { mensagem = "Credenciais inválidas." });
                }

                var claims = new[]
                {
                    new Claim("id", usuarioExistente.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuarioExistente.Nome),
                    new Claim(ClaimTypes.Email, usuarioExistente.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    mensagem = "Login realizado com sucesso.",
                    token = tokenString
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

        [HttpPut("esqueci-senha")]
        public async Task<IActionResult> AtualizarSenha([FromBody] Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    return BadRequest(new { mensagem = "Email e nova senha são obrigatórios." });
                }

                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email);

                if (usuarioExistente == null)
                {
                    return NotFound(new { mensagem = "Usuário não encontrado." });
                }

                usuarioExistente.Senha = usuario.Senha;

                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Senha atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao atualizar a senha.",
                    erro = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Email))
                {
                    return BadRequest(new { mensagem = "Email é obrigatório." });
                }
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha);
                if (usuarioExistente == null)
                {
                    return NotFound(new { mensagem = "Usuário não encontrado." });
                }
                _context.Usuarios.Remove(usuarioExistente);
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Usuário excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao excluir usuário.",
                    erro = ex.Message
                });
            }
        }
    }
}


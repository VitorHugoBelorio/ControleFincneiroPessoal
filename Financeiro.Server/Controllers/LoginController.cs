using Financeiro.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.Server.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        // Simulando banco de dados
        private static readonly List<Usuario> usuarios = new()
        {
            new Usuario { Email = "teste@email.com", Senha = "123456" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario login)
        {
            var usuario = usuarios.FirstOrDefault
                (x => x.Email == login.Email && x.Senha == login.Senha);

            if (usuario == null)
            {
                return Unauthorized(new { mensagem = "Credenciais inválidas" });
            }

            // Aqui você poderia gerar um token JWT real
            var fakeToken = "tokenFake123";

            return Ok(new
            {
                mensagem = "Login realizado com sucesso",
                usuario = new { usuario.Email },
                token = fakeToken
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using BLL;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario model)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var item in ModelState.Values)
                {
                    errors.AddRange(item.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                }

                return BadRequest(errors);
            }

            var service = new UsuarioService();

            // Recupera o usuário
            var user = await service.Get(model.Nome, model.Senha);

            // Verifica se o usuário existe
            if (user is null)
                return NotFound("Verify your user and password!");

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Senha = "";

            // Retorna os dados
            return Ok(new
            {
                user = new
                {
                    user,
                    token
                }
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Request.HttpContext.User = null;

            return NoContent();
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "funcionario,admin")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Manager() => "Admin";
    }
}

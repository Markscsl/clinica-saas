using Clinica.Application.Auth.Commands.AlterarSenha;
using Clinica.Application.Auth.Commands.LoginUsuario;
using Clinica.Application.Auth.Commands.RegistrarUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(new { Token = token });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("criar-interno")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CriarUsuarioInterno([FromBody] RegistrarUsuarioCommand command)
        {
            var rolesPermitidas = new[] { "Admin", "Secretaria" };

            if (!rolesPermitidas.Contains(command.Role))
            {
                return BadRequest("Cargo inválido. Escolha entre: Admin ou Secretaria.");
            }

            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { Id = id, Message = $"Usuario {command.Role} criado com sucesso!" });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("alterar-senha")]
        [Authorize]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaCommand alterarSenhaCommand)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                alterarSenhaCommand.UsuarioId = Guid.Parse(userId);

                await _mediator.Send(alterarSenhaCommand);

                return Ok(new { Message = "Senha alterada com sucesso! Verifique seu e-mail." });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

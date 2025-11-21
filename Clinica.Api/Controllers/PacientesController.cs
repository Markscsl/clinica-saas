using Clinica.Application.Pacientes.Commands.CriarPaciente;
using Clinica.Application.Pacientes.Commands.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationException = FluentValidation.ValidationException;

namespace Clinica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPaciente([FromBody] CriarPacienteCommand command)
        {
            try
            {
                var pacienteId = await _mediator.Send(command);
                return Ok(pacienteId);
            }

            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Secretaria")]
        public async Task<IActionResult> ListarTodos()
        {
            var resultado = await _mediator.Send(new ObterTodosPacientesQuery());
            return Ok(resultado);
        }
    }
}

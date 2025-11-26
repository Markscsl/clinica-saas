using Clinica.Application.Medicos.Commands.CriarMedico;
using Clinica.Application.Medicos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AdicionarMedico([FromBody] CriarMedicoCommand command)
        {
            try
            {
                var medicoId = await _mediator.Send(command);
                return Ok(medicoId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> ListarTodos([FromQuery] Guid? especialidadeId)
        {
            var query = new ObterTodosMedicosQuery(especialidadeId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

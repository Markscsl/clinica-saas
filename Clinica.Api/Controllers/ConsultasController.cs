using Clinica.Application.Consultas.Commands.AgendarNovaConsulta;
using Clinica.Application.Consultas.Commands.CancelarConsulta;
using Clinica.Application.Consultas.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("agendar")]

        public async Task<IActionResult> AgendarNovaConsutla([FromBody] AgendarNovaConsultaCommand command)
        {
            try
            {
                var consultaId = await _mediator.Send(command);

                return CreatedAtAction(nameof(ObterConsultaPorId), new { id = consultaId }, command);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}/cancelar")]

        public async Task<IActionResult> CancelarConsulta(Guid id)
        {
            try
            {
                var command = new CancelarConsultaCommand(id);

                await _mediator.Send(command);

                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterConsultaPorId(Guid id)
        {

            var query = new ObterConsultaPorIdQuery(id);

            var consulta = await _mediator.Send(query);

            if (consulta == null)
            {
                return NotFound();
            }

            return Ok(consulta);
        }
    }
}

using Clinica.Application.Especialidades.Commands.CriarEspecialidade;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EspecialidadesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarEspecialidade([FromBody] CriarEspecialidadeCommand command)
        {
            try
            {
                var especialidadeId = await _mediator.Send(command);
                return Ok(especialidadeId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

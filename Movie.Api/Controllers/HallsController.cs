using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Contracts.Halls;
using Movie.Application.Features.Halls.Commands.ChangeSeatType;
using Movie.Application.Features.Halls.Commands.CreateHall;
using Movie.Application.Features.Halls.Commands.DeleteHall;
using Movie.Application.Features.Halls.Commands.RestoreHall;
using Movie.Application.Features.Halls.Commands.UpdateHall;
using Movie.Application.Features.Halls.Queries.GetHall;
using Movie.Application.Features.Halls.Queries.GetHalls;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        private readonly ISender _sender;

        public HallsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetHallsResponse>>> GetHalls(
            [FromQuery] bool includeDeleted = false,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(
                new GetHallsQuery(includeDeleted),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetHallResponse>> GetHall(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new GetHallQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateHall(
            CreateHallCommand command,
            CancellationToken cancellationToken)
        {
            Guid id = await _sender.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetHall),
                new { id },
                id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHall(
            Guid id,
            UpdateHallCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest();

            await _sender.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{hallId:guid}/seats/{seatId:guid}/type")]
        public async Task<IActionResult> ChangeSeatType(
            Guid hallId,
            Guid seatId,
            [FromBody] ChangeSeatTypeRequest request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new ChangeSeatTypeCommand(
                    hallId,
                    seatId,
                    request.Type),
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHall(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new DeleteHallCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> RestoreHall(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RestoreHallCommand(id),
                cancellationToken);

            return NoContent();
        }
    }
}

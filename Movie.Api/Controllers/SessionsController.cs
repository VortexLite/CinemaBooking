using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Contracts.Sessions;
using Movie.Application.Features.Sessions.Commands.CancelSession;
using Movie.Application.Features.Sessions.Commands.CreateSession;
using Movie.Application.Features.Sessions.Commands.DeleteSession;
using Movie.Application.Features.Sessions.Commands.FinishSession;
using Movie.Application.Features.Sessions.Commands.OpenSessionBooking;
using Movie.Application.Features.Sessions.Commands.RestoreSession;
using Movie.Application.Features.Sessions.Commands.StartSession;
using Movie.Application.Features.Sessions.Commands.UpdateSession;
using Movie.Application.Features.Sessions.Queries.GetSession;
using Movie.Application.Features.Sessions.Queries.GetSessions;
using Movie.Domain.Enums;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISender _sender;

        public SessionsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessions(
            [FromQuery] DateTime? fromUtc = null,
            [FromQuery] DateTime? toUtc = null,
            [FromQuery] Guid? movieId = null,
            [FromQuery] Guid? hallId = null,
            [FromQuery] SessionStatus? status = null,
            [FromQuery] bool includeDeleted = false,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(
                new GetSessionsQuery(
                    fromUtc,
                    toUtc,
                    movieId,
                    hallId,
                    status,
                    includeDeleted),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSession(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new GetSessionQuery(id),
                cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(
            [FromBody] CreateSessionRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateSessionCommand(
                request.MovieId,
                request.HallId,
                request.StartTime,
                request.Price,
                request.ProjectionType,
                request.SubtitleType,
                request.SoundFormat);

            Guid id = await _sender.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetSession),
                new { id },
                new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSession(
            Guid id,
            [FromBody] UpdateSessionRequest request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new UpdateSessionCommand(
                    id,
                    request.HallId,
                    request.StartTime,
                    request.Price,
                    request.ProjectionType,
                    request.SubtitleType,
                    request.SoundFormat),
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSession(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new DeleteSessionCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> RestoreSession(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RestoreSessionCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/open-booking")]
        public async Task<IActionResult> OpenBooking(
    Guid id,
    CancellationToken cancellationToken)
        {
            await _sender.Send(
                new OpenSessionBookingCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(
            Guid id,
            [FromBody] CancelSessionRequest request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new CancelSessionCommand(
                    id,
                    request.Reason),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/start")]
        public async Task<IActionResult> Start(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new StartSessionCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/finish")]
        public async Task<IActionResult> Finish(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new FinishSessionCommand(id),
                cancellationToken);

            return NoContent();
        }
    }
}

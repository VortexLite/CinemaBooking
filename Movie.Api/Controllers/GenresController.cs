using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Features.Genres.Commands.CreateGenre;
using Movie.Application.Features.Genres.Commands.DeleteGenre;
using Movie.Application.Features.Genres.Commands.RestoreGenre;
using Movie.Application.Features.Genres.Commands.UpdateGenre;
using Movie.Application.Features.Genres.Queries.GetGenre;
using Movie.Application.Features.Genres.Queries.GetGenres;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ISender _sender;

        public GenresController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetGenresResponse>>> GetGenres(
            [FromQuery] bool includeDeleted = false,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(new GetGenresQuery(includeDeleted),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetGenreResponse>> GetGenre(
        Guid id,
        CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new GetGenreQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateGenre(
            [FromBody] CreateGenreCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _sender.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetGenre),
                new { id },
                id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGenre(
            Guid id,
            [FromBody] UpdateGenreCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest();

            await _sender.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGenre(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new DeleteGenreCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> RestoreGenre(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RestoreGenreCommand(id),
                cancellationToken);

            return NoContent();
        }
    }
}

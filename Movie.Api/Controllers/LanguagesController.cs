using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Features.Languages.Commands.CreateLanguage;
using Movie.Application.Features.Languages.Commands.DeleteLanguage;
using Movie.Application.Features.Languages.Commands.RestoreLanguage;
using Movie.Application.Features.Languages.Commands.UpdateLanguage;
using Movie.Application.Features.Languages.Queries.GetLanguage;
using Movie.Application.Features.Languages.Queries.GetLanguages;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ISender _sender;

        public LanguagesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetLanguagesResponse>>> GetLanguages(
            [FromQuery] bool includeDeleted = false,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(
                new GetLanguagesQuery(includeDeleted),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetLanguageResponse>> GetLanguage(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new GetLanguageQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateLanguage(
            CreateLanguageCommand command,
            CancellationToken cancellationToken)
        {
            Guid id = await _sender.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetLanguage),
                new { id },
                id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLanguage(
            Guid id,
            UpdateLanguageCommand command,
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
        public async Task<IActionResult> DeleteLanguage(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new DeleteLanguageCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> RestoreLanguage(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RestoreLanguageCommand(id),
                cancellationToken);

            return NoContent();
        }
    }
}

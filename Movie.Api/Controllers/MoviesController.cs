using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Contracts.Movies;
using Movie.Application.Features.Movies.Commands.ArchiveMovie;
using Movie.Application.Features.Movies.Commands.CreateMovie;
using Movie.Application.Features.Movies.Commands.DeleteMovie;
using Movie.Application.Features.Movies.Commands.PublishMovie;
using Movie.Application.Features.Movies.Commands.RestoreMovie;
using Movie.Application.Features.Movies.Commands.UpdateMovie;
using Movie.Application.Features.Movies.Queries.GetMovie;
using Movie.Application.Features.Movies.Queries.GetMovies;
using Movie.Domain.Enums;
using Shared.Common.Models;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ISender _sender;

        public MoviesController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Отримати всі фільми+
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetMovies(
            [FromQuery] GetMoviesRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetMoviesQuery(
                request.Search,
                request.Status,
                request.GenreId,
                request.LanguageId,
                request.IncludeDeleted,
                request.Page,
                request.PageSize);

            return Ok(await _sender.Send(query,
                cancellationToken));
        }

        /// <summary>
        /// Отримати фільм по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetMovieResponse>> GetMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            var movie = await _sender.Send(
                new GetMovieQuery(id),
                cancellationToken);

            return Ok(movie);
        }

        /// <summary>
        /// Створити фільм
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMovie(
            [FromBody] CreateMovieCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _sender.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetMovie),
                new { id },
                id);
        }

        /// <summary>
        /// Оновити фільм
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMovie(
            Guid id,
            [FromBody] UpdateMovieCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest();

            await _sender.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Видалити фільм
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new DeleteMovieCommand(id),
                cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Опублікувати фільм
        /// </summary>
        [HttpPatch("{id:guid}/publish")]
        public async Task<IActionResult> PublishMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new PublishMovieCommand(id),
                cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Отримати всі опубліковані фільми
        /// </summary>
        [HttpGet("published")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetPublishedMovies(
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    Search: null,
                    Status: MovieStatus.Published,
                    GenreId: null,
                    LanguageId: null,
                    IncludeDeleted: false,
                    Page: 1,
                    PageSize: 20),
                cancellationToken));
        }

        /// <summary>
        /// Архівувати фільм
        /// </summary>
        [HttpPatch("{id:guid}/archive")]
        public async Task<IActionResult> ArchiveMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new ArchiveMovieCommand(id),
                cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Отримати архівні фільми
        /// </summary>
        [HttpGet("archived")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetArchivedMovies(
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    null,
                    MovieStatus.Archived,
                    null,
                    null,
                    false,
                    1,
                    20),
                cancellationToken));
        }

        /// <summary>
        /// Отримати всі чернетки
        /// </summary>
        [HttpGet("drafts")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetDraftMovies(
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    null,
                    MovieStatus.Draft,
                    null,
                    null,
                    false,
                    1,
                    20),
                cancellationToken));
        }

        /// <summary>
        /// Відновити видалений фільм
        /// </summary>
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> RestoreMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RestoreMovieCommand(id),
                cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Отримати видалені фільми
        /// </summary>
        [HttpGet("deleted")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetDeletedMovies(
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    null,
                    null,
                    null,
                    null,
                    true,
                    1,
                    20),
                cancellationToken));
        }

        /// <summary>
        /// Пошук фільмів
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> SearchMovies(
            [FromQuery] string search,
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    search,
                    null,
                    null,
                    null,
                    false,
                    1,
                    20),
                cancellationToken));
        }

        /// <summary>
        /// Отримати фільми за жанром
        /// </summary>
        [HttpGet("genre/{genreId:guid}")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetByGenre(
            Guid genreId,
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    null,
                    null,
                    genreId,
                    null,
                    false,
                    1,
                    20),
                cancellationToken));
        }

        /// <summary>
        /// Отримати фільми за мовою
        /// </summary>
        [HttpGet("language/{languageId:guid}")]
        public async Task<ActionResult<PagedResult<GetMoviesResponse>>> GetByLanguage(
            Guid languageId,
            CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(
                new GetMoviesQuery(
                    null,
                    null,
                    null,
                    languageId,
                    false,
                    1,
                    20),
                cancellationToken));
        }
    }
}

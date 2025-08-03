using Microsoft.AspNetCore.Mvc;
using MovieContracts;

namespace MoviePresentation.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ActorsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        //private readonly MovieApiContext _context;

        //public ActorsController(MovieApiContext context)
        //{
        //    _context = context;
        //}

        // POST: api/actors/{actorId}/movies/{movieId}
        [HttpPost("{actorId}/movies/{movieId}")]
        public async Task<IActionResult> AddActorToMovie(int actorId, int movieId)
        {
            try
            {
                await serviceManager.ActorService.AddActorToMovieAsync(actorId, movieId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/actors/{actorId}/movies/{movieId}
        //[HttpPost("{actorId}/movies/{movieId}")]
        //public async Task<IActionResult> AddActorToMovie(int actorId, int movieId)
        //{
        //    var movie = await _context.Movies
        //        .Include(m => m.Actors)
        //        .FirstOrDefaultAsync(m => m.Id == movieId);
        //    if (movie == null)
        //        return NotFound($"Filmen med id {movieId} hittades ej.");

        //    var actor = await _context.Actors.FindAsync(actorId);
        //    if (actor == null)
        //        return NotFound($"Skådespelaren med id {actorId} hittades ej.");

        //    if (movie.Actors.Any(a => a.Id == actorId))
        //        return BadRequest("Skådespelaren finns redan registrerad på filmen.");

        //    movie.Actors.Add(actor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

    }
}

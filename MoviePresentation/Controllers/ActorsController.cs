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

    }
}

using System.Text.Json.Nodes;
using ConsumeApi.Interface;
using ConsumeApi.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{

    public class MovieController : BaseApiController
    {  
        
        public IMovieServices _movieServices;
        public MovieController(IMovieServices movieServices, IMediator mediator): base(mediator)
        {
            _movieServices = movieServices;

        }

        /// <summary>
        /// Retrieves a specific list of movies by query
        /// </summary>
        [ProducesResponseType(typeof(ListMovies), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetMoviesByName")]
        public async Task<ActionResult<ListMovies>> GetMoviesByName([FromQuery]ParametresQuery parametresQuery){
            var result = await _movieServices.GetAllMoviesByName(parametresQuery);

            if(result == null) return ValidationProblem();
        
            return result;
        }

        /// <summary>
        /// Retrieves popular movies 
        /// </summary>
        [HttpGet("GetPopularMovies")]
        public async Task<ActionResult<ListMovies>> GetPopularMovies([FromQuery] ParametresQuery parametresQuery){
            var result = await _movieServices.GetPopularMovies(parametresQuery);

            if(result == null) return ValidationProblem();

            return result;
        }
    }
}
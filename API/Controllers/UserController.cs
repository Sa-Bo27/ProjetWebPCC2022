using Application.Commands.UserCommand;
using Application.Queries;
using Application.RequestHelpers;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }
        
        /// <summary>
        /// Retrieves a specific user by unique id or email
        /// </summary>
        [HttpGet("GetUserList")]
        [Authorize("read:user")]
        public async Task<ActionResult<List<MovieDto>>> GetUserList(string email)
        {

            if (email != null) return await _mediator.Send(new GetUserListQuery { Email = email });

            return NotFound();

        }

        /// <summary>
        /// Put a specific movie in a user list 
        /// </summary>
        [HttpPut("UpdateUserMovieList/{id}")]
        [Authorize("write:movie")]
        public async Task<ActionResult> UpdateUserMovieList( int id, UpdateUserMovieListCommand command)
        {
            
            if (id != command.Id) return BadRequest();

            await _mediator.Send(command);

            return NoContent();


        }

        /// <summary>
        /// Delete a specific movie from a user list 
        /// </summary>
        [HttpDelete("DeleteMovieFromList")]
        public async Task<ActionResult> DeleteMovie( [FromQuery] DeleteUserMovieListCommand command)
        {
            if(command.MovieId<=0) return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("claims")]
        public IActionResult Claims(){
            return Ok(User.Claims.Select(c => new {
                c.Type,
                c.Value
                
            }));
        }
    }
}
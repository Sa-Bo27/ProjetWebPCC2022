using Application.Commands.UserCommand;
using Application.Queries;
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
        [HttpGet("GetUser")]
        [Authorize("read:user")]
        public async Task<ActionResult<User>> GetUser( string email)
        {

            

            if (email != null) return await _mediator.Send(new GetUserQuery { Email = email });

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
        [HttpDelete("DeleteMovieFromList/{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {

            await _mediator.Send(id);

            return NoContent();
        }
    }
}
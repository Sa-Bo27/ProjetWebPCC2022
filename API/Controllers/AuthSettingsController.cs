using Application.Common.Models;
using Application.Queries.AuthSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthSettingsController : BaseApiController
    {
        public AuthSettingsController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("Auth0")]
        public async Task<ActionResult<AuthSettingsModel>> GetPublicAuthSettings(){

            var dto = await _mediator.Send(new GetPublicAuthSettingsQuery());

            if(dto != null) return dto;

            return NotFound();
        }
    }
}
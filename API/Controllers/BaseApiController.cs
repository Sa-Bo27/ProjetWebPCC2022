using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;

        }
    }
}
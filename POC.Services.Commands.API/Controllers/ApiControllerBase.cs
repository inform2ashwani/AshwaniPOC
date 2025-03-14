using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace POC.Services.Commands.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        //private ISender? _mediator;
        //protected ISender? Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
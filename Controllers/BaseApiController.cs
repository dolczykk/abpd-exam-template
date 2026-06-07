using Apbd.Shared.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Apbd.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController(IDispatcher dispatcher) : ControllerBase
{
    protected readonly IDispatcher Dispatcher = dispatcher;
}
using System.Net;
using AutoMapper;
using Kvpbldsck.NastolochkiAPI.Common.Contract.Extensions;
using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;
using Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private const int NoResponse = 444;

    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventsController(
        IEventService eventService,
        IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(ICollection<EventVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var events = await _eventService.GetAsync(cancellationToken).ConfigureAwait(false);
        var vm = _mapper.Map<ICollection<EventVm>>(events);

        if (cancellationToken.IsCancellationRequested)
        {
            return NoContent();
        }

        return Ok(vm);
    }

    [HttpGet("{guid:guid}")]
    [ProducesResponseType(typeof(EventVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var @event = await _eventService.GetAsync(guid, cancellationToken).ConfigureAwait(false);
        var vm = _mapper.Map<EventVm>(@event);

        if (cancellationToken.IsCancellationRequested)
        {
            return NoContent();
        }

        return Ok(vm);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), NoResponse)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] EventCreateVm @event, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(@event, new Event { Guid = Guid.NewGuid() });
        var guid = await _eventService.AddAsync(model, cancellationToken).ConfigureAwait(false);

        if (cancellationToken.IsCancellationRequested)
        {
            return NoContent();
        }

        if (guid == Guid.Empty)
        {
            return StatusCode(NoResponse);
        }

        var uri = HttpContext.Request.Path.Value.Append(guid) ?? string.Empty;
        return Created(uri, guid);
    }
}

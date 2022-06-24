using AutoMapper;
using Kvpbldsck.NastolochkiAPI.Common.Contract.Extensions;
using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;
using Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Controllers;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private const int NoResponse = 444;

    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;

    public LocationController(ILocationService locationService, IMapper mapper)
    {
        _locationService = locationService;
        _mapper = mapper;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(ICollection<LocationVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var locations = await _locationService.GetAsync(cancellationToken).ConfigureAwait(false);
        var vm = _mapper.Map<ICollection<LocationVm>>(locations);

        if (cancellationToken.IsCancellationRequested)
        {
            return NoContent();
        }

        return Ok(vm);
    }

    [HttpGet("{guid:guid}")]
    [ProducesResponseType(typeof(LocationVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetAsync(guid, cancellationToken).ConfigureAwait(false);
        var vm = _mapper.Map<LocationVm>(location);

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
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] LocationCreateVm location, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(location, new Location { Guid = Guid.NewGuid() });
        var guid = await _locationService.AddAsync(model, cancellationToken).ConfigureAwait(false);

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

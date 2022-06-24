using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public sealed class EventCreateVm : EventBaseVm
{
    public Guid LocationGuid { get; init; }
}

public sealed class EventCreateValidator : EventBaseValidator<EventCreateVm>
{
    public EventCreateValidator()
    {
        RuleFor(e => e.LocationGuid)
            .NotEqual(Guid.Empty).WithMessage(string.Format(Localization.LocationNotFound, Guid.Empty));
    }
}

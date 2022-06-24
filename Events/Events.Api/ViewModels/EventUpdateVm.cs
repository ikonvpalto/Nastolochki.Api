using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public sealed class EventUpdateVm : EventBaseVm
{
    public Guid Guid { get; init; }

    public Guid LocationGuid { get; init; }
}

public sealed class EventUpdateValidator : EventBaseValidator<EventUpdateVm>
{
    public EventUpdateValidator()
    {
        RuleFor(e => e.Guid)
            .NotEqual(Guid.Empty).WithMessage(string.Format(Localization.EventNotFound, Guid.Empty));

        RuleFor(e => e.LocationGuid)
            .NotEqual(Guid.Empty).WithMessage(string.Format(Localization.LocationNotFound, Guid.Empty));
    }
}

using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public sealed class EventVm : EventBaseVm
{
    public Guid Guid { get; init; }

    public LocationVm Location { get; init; }
}

public sealed class EventValidator : EventBaseValidator<EventVm>
{
    public EventValidator()
    {
        RuleFor(e => e.Guid)
            .NotEqual(Guid.Empty).WithMessage(string.Format(Localization.EventNotFound, Guid.Empty));

        RuleFor(e => e.Location)
            .SetValidator(new LocationValidator());
    }
}

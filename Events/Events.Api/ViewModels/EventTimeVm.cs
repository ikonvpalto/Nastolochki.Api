using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public sealed class EventTimeVm
{
    public bool IsVoting { get; init; }

    public bool IsVoted { get; init; }

    public ICollection<DateTime> TimeVariants { get; init; }
}

public sealed class EventTimeValidator : AbstractValidator<EventTimeVm>
{
    public EventTimeValidator()
    {
        RuleFor(et => et.TimeVariants)
            .NotEmpty().WithMessage(Localization.EventTimeDatesIsEmpty)
            .Must(ev => ev.All(v => v.Date >= DateTime.UtcNow)).WithMessage(Localization.EventOutdated);
    }
}


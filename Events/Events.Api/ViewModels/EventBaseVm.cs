using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public abstract class EventBaseVm
{
    public string Name { get; init; }

    public string Description { get; init; }

    public EventTimeVm Time { get; init; }

    public ICollection<Guid> Participants { get; init; }
}

public abstract class EventBaseValidator<T> : AbstractValidator<T> where T : EventBaseVm
{
    protected EventBaseValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().Must(n => !string.IsNullOrWhiteSpace(n)).WithMessage(Localization.EventNameIsEmpty)
            .MaximumLength(200).WithMessage(Localization.EventNameTooLong);

        RuleFor(e => e.Description)
            .MaximumLength(2000).WithMessage(Localization.EventDescriptionTooLong);

        RuleFor(e => e.Time)
            .SetValidator(new EventTimeValidator());

        RuleFor(et => et.Participants)
            .NotEmpty().WithMessage(Localization.EventNoParticipants);
    }
}

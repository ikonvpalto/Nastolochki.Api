using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public abstract class LocationBaseVm
{
    public string Address { get; init; }

    public string Name { get; init; }
}

public abstract class LocationBaseValidator<T> : AbstractValidator<T> where T : LocationBaseVm
{
    protected LocationBaseValidator()
    {
        RuleFor(e => e.Address)
            .NotEmpty().Must(n => !string.IsNullOrWhiteSpace(n)).WithMessage(Localization.LocationAddressIsEmpty)
            .MaximumLength(300).WithMessage(Localization.LocationAddressTooLong);

        RuleFor(e => e.Name)
            .MaximumLength(300).WithMessage(Localization.LocationNameTooLong);
    }
}

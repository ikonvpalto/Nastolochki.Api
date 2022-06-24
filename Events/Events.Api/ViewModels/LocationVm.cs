using FluentValidation;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

public sealed class LocationVm : LocationBaseVm
{
    public Guid Guid { get; init; }
}

public sealed class LocationValidator : LocationBaseValidator<LocationVm>
{
    public LocationValidator()
    {
        RuleFor(e => e.Guid)
            .NotEqual(Guid.Empty).WithMessage(string.Format(Localization.LocationNotFound, Guid.Empty));
    }
}

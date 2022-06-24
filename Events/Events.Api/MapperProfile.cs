using AutoMapper;
using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Kvpbldsck.NastolochkiAPI.Events.Api.ViewModels;

namespace Kvpbldsck.NastolochkiAPI.Events.Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<EventVm, Event>()
            .ForMember(m => m.Description, o => o.MapFrom(vm => vm.Description))
            .ForMember(m => m.Guid, o => o.MapFrom(vm => vm.Guid))
            .ForMember(m => m.Name, o => o.MapFrom(vm => vm.Name))
            .ForMember(m => m.Location, o => o.MapFrom(vm => vm.Location))
            .ForMember(m => m.LocationGuid, o => o.MapFrom(vm => vm.Location.Guid))
            .ForMember(m => m.Participants, o => o.MapFrom(vm => vm.Participants.Select(p => Tuple.Create(p, vm.Guid))))
            .ForMember(m => m.Time, o => o.MapFrom(vm => Tuple.Create(vm.Time, vm.Guid)))
            .ReverseMap()
            .ForMember(vm => vm.Description, o => o.MapFrom(m => m.Description))
            .ForMember(vm => vm.Guid, o => o.MapFrom(m => m.Guid))
            .ForMember(vm => vm.Location, o => o.MapFrom(m => m.Location))
            .ForMember(vm => vm.Name, o => o.MapFrom(m => m.Name))
            .ForMember(vm => vm.Participants, o => o.MapFrom(m => m.Participants.Select(p => p.ParticipantGuid)))
            .ForMember(vm => vm.Time, o => o.MapFrom(m => m.Time));

        CreateMap<EventCreateVm, Event>()
            .ForMember(m => m.Guid, o => o.Ignore())
            .ForMember(m => m.Description, o => o.MapFrom(vm => vm.Description))
            .ForMember(m => m.Name, o => o.MapFrom(vm => vm.Name))
            .ForMember(m => m.Location, o => o.Ignore())
            .ForMember(m => m.LocationGuid, o => o.MapFrom(vm => vm.LocationGuid))
            .ForMember(m => m.Participants, o => o.MapFrom((vm, m) => vm.Participants.Select(p => Tuple.Create(p, m.Guid))))
            .ForMember(m => m.Time, o => o.MapFrom((vm, m) => Tuple.Create(vm.Time, m.Guid)));

        CreateMap<EventUpdateVm, Event>()
            .ForMember(m => m.Guid, o => o.MapFrom(vm => vm.Guid))
            .ForMember(m => m.Description, o => o.MapFrom(vm => vm.Description))
            .ForMember(m => m.Name, o => o.MapFrom(vm => vm.Name))
            .ForMember(m => m.Location, o => o.Ignore())
            .ForMember(m => m.LocationGuid, o => o.MapFrom(vm => vm.LocationGuid))
            .ForMember(m => m.Participants, o => o.MapFrom((vm, m) => vm.Participants.Select(p => Tuple.Create(p, m.Guid))))
            .ForMember(m => m.Time, o => o.MapFrom((vm, m) => Tuple.Create(vm.Time, m.Guid)));

        CreateMap<LocationVm, Location>()
            .ForMember(m => m.Guid, o => o.MapFrom((vm, _) => vm.Guid == Guid.Empty ? Guid.NewGuid() : vm.Guid))
            .ForMember(m => m.Address, o => o.MapFrom(vm => vm.Address))
            .ForMember(m => m.Name, o => o.MapFrom(vm => vm.Name))
            .ReverseMap()
            .ForMember(vm => vm.Guid, o => o.MapFrom(m => m.Guid))
            .ForMember(vm => vm.Address, o => o.MapFrom(m => m.Address))
            .ForMember(vm => vm.Name, o => o.MapFrom(m => m.Name));

        CreateMap<LocationCreateVm, Location>()
            .ForMember(m => m.Guid, o => o.Ignore())
            .ForMember(m => m.Address, o => o.MapFrom(vm => vm.Address))
            .ForMember(m => m.Name, o => o.MapFrom(vm => vm.Name));

        CreateMap<Tuple<Guid, Guid>, EventParticipant>()
            .ForMember(m => m.EventGuid, o => o.MapFrom(vm => vm.Item2))
            .ForMember(m => m.ParticipantGuid, o => o.MapFrom(vm => vm.Item1));

        CreateMap<Tuple<EventTimeVm, Guid>, EventTime>()
            .ForMember(m => m.IsVoted, o => o.MapFrom(vm => vm.Item1.IsVoted))
            .ForMember(m => m.IsVoting, o => o.MapFrom(vm => vm.Item1.IsVoting))
            .ForMember(m => m.TimeVariants, o => o.MapFrom(vm => vm.Item1.TimeVariants.Select(t => Tuple.Create(t, vm.Item2))));

        CreateMap<EventTime, EventTimeVm>()
            .ForMember(vm => vm.IsVoted, o => o.MapFrom(m => m.IsVoted))
            .ForMember(vm => vm.IsVoting, o => o.MapFrom(m => m.IsVoting))
            .ForMember(vm => vm.TimeVariants, o => o.MapFrom(m => m.TimeVariants.Select(t => t.Time)));

        CreateMap<Tuple<DateTime, Guid>, EventTimeVariant>()
            .ForMember(m => m.Time, o => o.MapFrom(vm => vm.Item1))
            .ForMember(m => m.EventGuid, o => o.MapFrom(vm => vm.Item2));
    }
}

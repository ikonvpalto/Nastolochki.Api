using System.Linq.Expressions;
using Kvpbldsck.NastolochkiAPI.Common.Db.Repositories;
using Kvpbldsck.NastolochkiAPI.Events.Api.Database;
using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Kvpbldsck.NastolochkiAPI.Events.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Repositories;

public sealed class EventsRepository : BaseEntityRepository<Event>, IEventsRepository
{
    public EventsRepository(EventsDbContext dbContext)
        : base(
            dbContext,
            dbContext.Events,
            dbContext.Events
                .Include(e => e.Location)
                .Include(e => e.Participants)
                .Include(e => e.Time.TimeVariants)
        )
    {
    }

    public override async Task<bool> CreateAsync(Event model, CancellationToken cancellationToken)
    {
        model = CleanLocationField(model);
        return await base.CreateAsync(model, cancellationToken);
    }

    public override async Task<bool> CreateAsync(ICollection<Event> models, CancellationToken cancellationToken)
    {
        models = models.Select(CleanLocationField).ToList();
        return await base.CreateAsync(models, cancellationToken);
    }

    public override async Task<bool> UpdateAsync(Event model, CancellationToken cancellationToken)
    {
        model = CleanLocationField(model);
        return await base.UpdateAsync(model, cancellationToken);
    }

    private static Event CleanLocationField(Event model)
    {
        if (model.Location is not null)
        {
            model = new Event
            {
                Guid = model.Guid,
                Description = model.Description,
                Location = null,
                Name = model.Name,
                Participants = model.Participants,
                LocationGuid = model.LocationGuid,
                Time = model.Time
            };
        }

        return model;
    }
}

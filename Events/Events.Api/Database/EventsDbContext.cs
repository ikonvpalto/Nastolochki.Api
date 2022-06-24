using Kvpbldsck.NastolochkiAPI.Events.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Database;

public sealed class EventsDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    public DbSet<EventParticipant> EventParticipants { get; set; }

    public DbSet<EventTimeVariant> EventTimeVariants { get; set; }

    public DbSet<Location> Locations { get; set; }

    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(builder =>
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Guid).HasName("PK_Event");
            builder.Property(e => e.Guid).HasColumnName("Guid").IsRequired().ValueGeneratedNever();
            builder.Property(e => e.LocationGuid).HasColumnName("LocationGuid").IsRequired().ValueGeneratedNever();
            builder.Property(e => e.Name).HasColumnName("Name").HasColumnType("varchar(200)").HasMaxLength(200).IsRequired();
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("varchar(2000)").HasMaxLength(2000).IsRequired();

            var time = builder.OwnsOne(
                e => e.Time,
                b =>
                {
                    b.Property(et => et.IsVoted).HasColumnName("TimeIsVoted").HasColumnType("boolean").IsRequired();
                    b.Property(et => et.IsVoting).HasColumnName("TimeIsVoting").HasColumnType("boolean").IsRequired();
                    b.OwnsMany(
                        et => et.TimeVariants,
                        b =>
                        {
                            b.HasKey(et => new { et.EventGuid, et.Time }).HasName("PK_EventTimeVariant");
                            b.Property(et => et.EventGuid).HasColumnName("EventGuid").IsRequired().ValueGeneratedNever();
                            b.Property(et => et.Time).HasColumnName("Time").HasColumnType("timestamp").IsRequired();

                        });
                });

            builder.HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationGuid);
            builder.HasMany(e => e.Participants).WithOne();
        });

        modelBuilder.Entity<EventParticipant>(builder =>
        {
            builder.Property(ep => ep.EventGuid).HasColumnName("EventGuid").IsRequired().ValueGeneratedNever();
            builder.Property(ep => ep.ParticipantGuid).HasColumnName("ParticipantGuid").IsRequired().ValueGeneratedNever();

            builder.HasKey(ep => new { ep.EventGuid, ep.ParticipantGuid }).HasName("PK_EventParticipant");
        });

        modelBuilder.Entity<Location>(builder =>
        {
            builder.ToTable("Locations");

            builder.HasKey(l => l.Guid).HasName("PK_Location");
            builder.Property(l => l.Guid).HasColumnName("Guid").IsRequired().ValueGeneratedNever();
            builder.Property(l => l.Address).HasColumnName("Address").HasColumnType("varchar(300)").HasMaxLength(300).IsRequired();
            builder.Property(l => l.Name).HasColumnName("Name").HasColumnType("varchar(300)").HasMaxLength(300).IsRequired();
        });
    }
}

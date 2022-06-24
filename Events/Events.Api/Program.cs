using System.Net;
using System.Net.Mime;
using FluentValidation.AspNetCore;
using Kvpbldsck.NastolochkiAPI.Common.Contract.Configuration;
using Kvpbldsck.NastolochkiAPI.Common.Contract.Exceptions;
using Kvpbldsck.NastolochkiAPI.Events.Api.Database;
using Kvpbldsck.NastolochkiAPI.Events.Api.Repositories;
using Kvpbldsck.NastolochkiAPI.Events.Api.Repositories.Contracts;
using Kvpbldsck.NastolochkiAPI.Events.Api.Resources;
using Kvpbldsck.NastolochkiAPI.Events.Api.Services;
using Kvpbldsck.NastolochkiAPI.Events.Api.Services.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

const string dbName = "events-db";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EventsDbContext>(options => options.UseNpgsql(GetConnectionString(builder)));

builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.ImplicitlyValidateRootCollectionElements = true;
        fv.ImplicitlyValidateChildProperties = true;
        fv.RegisterValidatorsFromAssemblyContaining(typeof(Program));
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

RegisterServices(builder.Services);

var app = builder.Build();

ApplyMigrations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

AddExceptionHandling(app);

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IEventParticipantsRepository, EventParticipantsRepository>();
    services.AddScoped<IEventsRepository, EventsRepository>();
    services.AddScoped<IEventTimeVariantsRepository, EventTimeVariantsRepository>();
    services.AddScoped<ILocationsRepository, LocationsRepository>();

    services.AddScoped<IEventService, EventService>();
    services.AddScoped<ILocationService, LocationService>();
}

void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<EventsDbContext>().Database.Migrate();
}

void AddExceptionHandling(WebApplication webApplication)
{
    webApplication.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            var message = Localization.InternalError;
            var code = (int)HttpStatusCode.InternalServerError;

            if (exceptionHandlerPathFeature?.Error is BaseHttpException httpException)
            {
                code = (int)httpException.ErrorCode;
                message = httpException.Message;

                context.RequestServices.GetRequiredService<ILogger>().LogError(httpException, "");
            }
            else if (exceptionHandlerPathFeature?.Error is not null)
            {
                context.RequestServices.GetRequiredService<ILogger<Program>>().LogCritical(exceptionHandlerPathFeature.Error, "");
            }

            context.Response.StatusCode = code;
            context.Response.ContentType = MediaTypeNames.Text.Plain;
            await context.Response.WriteAsync(message);
        });
    });
}

string GetConnectionString(WebApplicationBuilder webApplicationBuilder)
{
    var connectionString = webApplicationBuilder.Configuration.GetConnectionString(dbName);

    if (connectionString is null)
    {
        webApplicationBuilder.Configuration.AddEnvFile("Properties/.env");
        connectionString = EventsDbContextFactory.CreateConnectionStringFromEnv(webApplicationBuilder.Configuration);
    }

    return connectionString;
}

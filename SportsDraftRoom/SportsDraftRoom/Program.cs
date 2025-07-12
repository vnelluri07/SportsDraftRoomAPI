using Microsoft.EntityFrameworkCore;
using SportsDraftRoom.Data.Context;
using SportsDraftRoom.Hubs;
using SportsDraftRoom.Internal;
using SportsDraftRoom.Repo;
using SportsDraftRoom.Repo.Implementation;
using SportsDraftRoom.Service;
using SportsDraftRoom.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISdrConfigurationService, SdrConfigurationService>();
builder.Services.AddDbContext<ISdrContext, SdrContext>((services, options) =>
{
    var cfgSvc = services.GetRequiredService<ISdrConfigurationService>();
    var env = services.GetRequiredService<IHostEnvironment>();

    options.UseSqlServer(cfgSvc.SdrConnection, builder => builder.EnableRetryOnFailure(
        5, TimeSpan.FromSeconds(60), null));
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React dev server origin
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Required for SignalR
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<IDraftRepo, DraftRepo>();
builder.Services.AddScoped<IDraftService, DraftService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<SdrHub>("/sdrhub");
app.MapHub<SdrHub2>("/sdr2hub");

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

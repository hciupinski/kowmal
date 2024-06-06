using System.Text.Json.Serialization;
using Kowmal.App.Clients;
using Kowmal.App.Clients.Interfaces;
using Kowmal.App.Components;
using Kowmal.App.MapperProfiles;
using Kowmal.App.Persistance;
using Kowmal.App.Persistance.Helpers;
using Kowmal.App.Services;
using Kowmal.App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContextFactory<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(AppDbContext.KowmalDb)}.db"));

builder.Services.AddAutoMapper(typeof(PostsProfile));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

#if DEBUG
builder.Services.AddScoped<IFileClient, LocalFileClient>();
#else
builder.Services.AddScoped<IFileService, FileService>();
#endif
builder.Services.AddScoped<IPhotoConverter, PhotoConverter>();
builder.Services.AddScoped<IPostService, PostService>();

// builder.Services.AddOidcAuthentication(options =>
// {
//     builder.Configuration.Bind("Local", options.ProviderOptions);
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<AppDbContext>>();
await AppDbContextHelpers.EnsureDbCreatedAndSeedWithCountOfAsync(options, 10);

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
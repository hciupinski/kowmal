using System.Text.Json.Serialization;
using Kowmal.API.Apis;
using Kowmal.API.Clients;
using Kowmal.API.Clients.Interfaces;
using Kowmal.API.Persistance;
using Kowmal.API.Persistance.Helpers;
using Kowmal.API.Services;
using Kowmal.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.AddEnvironmentVariables().Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//builder.Services.AddAuthorization();

//builder.Services.AddAntiforgery();

// Add a CORS policy for the client
// Add .AllowCredentials() for apps that use an Identity Provider for authn/z
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:5001", 
                builder.Configuration["FrontendUrl"] ?? "https://localhost:5002"])
            .AllowAnyMethod()
            .AllowAnyHeader()));

builder.Services.AddDbContextFactory<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(AppDbContext.KowmalDb)}.db"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

#if DEBUG
builder.Services.AddScoped<IFileClient, LocalFileClient>();
#else
builder.Services.AddScoped<IFileService, FileService>();
#endif
builder.Services.AddScoped<IPhotoConverter, PhotoConverter>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// This section sets up and seeds the database. Seeding is NOT normally
// handled this way in production. The following approach is used in this
// sample app to make the sample simpler. The app can be cloned. The
// connection string is configured. The app can be run.
await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<AppDbContext>>();
await AppDbContextHelpers.EnsureDbCreatedAndSeedWithCountOfAsync(options, 10);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthentication();
// app.UseAuthorization();

// Activate the CORS policy
app.UseCors("wasm");

app.UseHttpsRedirection();

//app.UseAntiforgery();

app.MapGroup("/api/posts")
    //.RequireAuthorization()
    .WithTags("Posts API")
    .MapPostsApi();

// app.MapGet("/api/antiforgery/token", (IAntiforgery forgeryService, HttpContext context) =>
// {
//     var tokens = forgeryService.GetAndStoreTokens(context);
//     var xsrfToken = tokens.RequestToken!;
//     return TypedResults.Content(xsrfToken, "text/plain");
// });

await app.RunAsync();
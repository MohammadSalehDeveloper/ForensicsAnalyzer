using System.Text;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi;
using ForensicsAnalyzer.Application;
using ForensicsAnalyzer.Infrastructure;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ForensicsAnalyzer.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your JWT token."
    });

    options.AddSecurityRequirement(_ =>
    {
        var req = new OpenApiSecurityRequirement();
        req[new OpenApiSecuritySchemeReference("Bearer", null)] = new List<string>();
        return req;
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:7123", "http://localhost:5123"); 
        // Update to match Blazor dev URLs
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ForensicsAnalyzer API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("BlazorClient");

app.MapControllers();

// Serve Blazor WebClient static files (development convenience)
var clientRoot = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "ForensicsAnalyzer.WebClient", "wwwroot"));
var clientFiles = new PhysicalFileProvider(clientRoot);

app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = clientFiles });
app.UseStaticFiles(new StaticFileOptions { FileProvider = clientFiles });

// Fallback to the client index.html for non-API requests (so SPA routes work)
app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api")
                   && !ctx.Request.Path.StartsWithSegments("/swagger")
                   && !ctx.Request.Path.StartsWithSegments("/_framework"),
    branch =>
    {
        branch.Run(async context =>
        {
            var index = clientFiles.GetFileInfo("index.html");
            if (index.Exists)
            {
                context.Response.ContentType = "text/html";
                using var stream = index.CreateReadStream();
                await stream.CopyToAsync(context.Response.Body);
            }
            else
            {
                context.Response.StatusCode = 404;
            }
        });
    });

await app.RunAsync();
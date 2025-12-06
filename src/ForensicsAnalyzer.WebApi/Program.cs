using ForensicsAnalyzer.Application;
using ForensicsAnalyzer.Infrastructure;
using ForensicsAnalyzer.Application.Cases;
using ForensicsAnalyzer.Contracts.Cases;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// later: app.UseAuthentication();
app.UseAuthorization();

app.UseCors("BlazorClient");

app.MapControllers();

app.Run();
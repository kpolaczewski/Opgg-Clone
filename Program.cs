using Microsoft.EntityFrameworkCore;
using RiotStatsAPI.Data;
using RiotStatsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();  // Adds OpenAPI for documentation

// Add CORS policy to allow frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173") // Frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});
//Add SQL server DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers (API routes)
builder.Services.AddControllers();


var apiKey = builder.Configuration["RiotApi:ApiKey"];

builder.Services.AddHttpClient();

builder.Services.AddTransient<RiotApiService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var dbContext = sp.GetRequiredService<AppDbContext>();
    return new RiotApiService(httpClient, apiKey, dbContext);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  // OpenAPI in development
}

app.UseHttpsRedirection();  // Enforces HTTPS
app.UseCors("AllowFrontend");  // Apply the CORS policy

// Map controllers (this will expose your API routes)
app.MapControllers();  // This is required for API routes

app.Run();

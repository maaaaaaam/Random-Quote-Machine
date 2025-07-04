using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

DotNetEnv.Env.Load("../.env");
var backend_port = Environment.GetEnvironmentVariable("VITE_BACKEND_PORT") ?? "5142";
var frontend_port = Environment.GetEnvironmentVariable("VITE_FRONTEND_PORT") ?? "5173";

builder.WebHost.UseUrls($"http://localhost:{backend_port}");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins($"http://localhost:{frontend_port}")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();

app.MapControllers();

app.Run();

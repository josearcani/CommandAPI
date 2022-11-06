using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommandContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

builder.Services.AddControllers();

// dependecy injection
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
// builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();

var app = builder.Build();

app.MapControllers();

app.Run();

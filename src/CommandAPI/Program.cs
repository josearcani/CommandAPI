using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var stringBuilder = new NpgsqlConnectionStringBuilder();
stringBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
stringBuilder.Username = builder.Configuration["UserID"];
stringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(opt =>
    opt.UseNpgsql(stringBuilder.ConnectionString));

builder.Services.AddControllers();

// dependecy injection
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
// builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();

var app = builder.Build();

app.MapControllers();

app.Run();

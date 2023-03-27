using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var stringBuilder = new NpgsqlConnectionStringBuilder();
stringBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
stringBuilder.Username = builder.Configuration["UserID"];
stringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(opt =>
    opt.UseNpgsql(stringBuilder.ConnectionString));

builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    // add support to patch operation
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});;

// add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// dependecy injection
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
// builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CommandContext>();
    db.Database.Migrate();
}

app.MapControllers();

app.Run();

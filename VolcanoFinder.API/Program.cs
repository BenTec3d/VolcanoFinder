using Microsoft.EntityFrameworkCore;
using VolcanoFinder.API.DbContexts;
using VolcanoFinder.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Warning! Move ConnectionString to safe location!
builder.Services.AddDbContext<VolcanoFinderContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:VolcanoFinderDbConnectionString"]));

builder.Services.AddScoped<IVolcanoFinderRepository, VolcanoFinderRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

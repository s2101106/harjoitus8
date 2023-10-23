using Microsoft.EntityFrameworkCore;
using harjoitus7;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SuperAdventureDBContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("SuperAdventureConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/superadventure", async (SuperAdventureDBContext context) => 
await context.Stats.ToListAsync());

app.MapGet("/superadventure/{id}", async (int id, SuperAdventureDBContext db) =>
await db.Stats.FindAsync(id)
is Stat stat
? Results.Ok(stat)
: Results.NotFound());

app.MapPut("/superadventure/{id}", async (SuperAdventureDBContext context,
    Stat stat, int id) =>
{
    var dbStat = await context.Stats.FindAsync(id);
    if (dbStat is null)
    {
        return Results.NotFound("Tilatietoja ei löydy!");
    }
    dbStat.CurrentHitpoints = stat.CurrentHitpoints;
    dbStat.MaxHitPoints = stat.MaxHitPoints;
    dbStat.Gold = stat.Gold;
    dbStat.Exp = stat.Exp;

    await context.SaveChangesAsync();
    return Results.Ok();
});

app.Run();


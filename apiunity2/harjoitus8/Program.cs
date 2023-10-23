using Microsoft.EntityFrameworkCore;
using harjoitus8;
//CREATE TABLE Quests (tehtavaId INTEGER PRIMARY KEY,
//tehtavaNimi TEXT, palkkioMaara INTEGER, tehtavaKuvaus TEXT,
//kokemusPisteet INTEGER, onkoAloitettu BIT, onkoSuoritettu BIT);
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UnityHarjoitusDBContext>(options=>options.UseSqlServer
(builder.Configuration.GetConnectionString("UnityHarjoitusConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/quest", async (UnityHarjoitusDBContext context) =>
await context.Quests.ToListAsync());

app.MapGet("/quest/{id}", async (int id, UnityHarjoitusDBContext db) =>
await db.Quests.FindAsync(id)
is Quest quest
? Results.Ok(quest)
: Results.NotFound());

app.MapGet("/quest/completed", async (UnityHarjoitusDBContext db) =>
await db.Quests.Where(x=>x.onkoSuoritettu==true).ToListAsync());

app.MapGet("/quest/inProgress", async (UnityHarjoitusDBContext db) =>
await db.Quests.Where(x => x.onkoSuoritettu == false && x.onkoAloitettu==true).ToListAsync());

app.MapPut("/quest/{id}", async (UnityHarjoitusDBContext context,
    Quest quest, int id) =>
{
    var dbQuest = await context.Quests.FindAsync(id);
    if (dbQuest is null)
    {
        return Results.NotFound("Tilatietoja ei löydy!");
    }
    dbQuest.tehtavaId = quest.tehtavaId;
    dbQuest.tehtavaNimi = quest.tehtavaNimi;
    dbQuest.palkkioMaara = quest.palkkioMaara;
    dbQuest.tehtavaKuvaus = quest.tehtavaKuvaus;
    dbQuest.kokemusPisteet=quest.kokemusPisteet;
    dbQuest.onkoAloitettu=quest.onkoAloitettu;
    dbQuest.onkoSuoritettu = quest.onkoSuoritettu;

    await context.SaveChangesAsync();
    return Results.Ok();
});

app.Run();

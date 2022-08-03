using Ensek.EnergyManager.Api.Commands;
using Ensek.EnergyManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("api"));

// our services
builder.Services
    .AddTransient<IMeterReadingService, MeterReadingService>()
    .AddTransient<ICsvParser, CsvParser>()
    .AddTransient<IMeterReadingsInsertCommand, MeterReadingsInsertCommand>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// endpoints
app.MapPost("/meter-reading-uploads", async (HttpRequest request, [FromServices] IMeterReadingService meterReadingService) =>
{
    if (!request.TryExtractFormFile(out var csvFile))
        return Results.BadRequest("File not found");

    if (csvFile == null)
        return Results.BadRequest("No file provided");

    var response = await meterReadingService.ProcessMeterReadingsCsvFileAsync(csvFile);

    return Results.Ok(response);
})
.WithName("PostMeterReadings");

// seeding
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApiContext>();
SeedAccountData(context);

// start!
app.Run();

static void SeedAccountData(ApiContext context)
{
    context.Accounts.AddRange(new[]
    {
        new AccountEntity("2344", new Name("Tommy", "Test")),
        new AccountEntity("2233", new Name("Barry", "Test")),
        new AccountEntity("8766", new Name("Sally", "Test")),
        new AccountEntity("2345", new Name("Jerry", "Test")),
        new AccountEntity("2346", new Name("Ollie", "Test")),
        new AccountEntity("2347", new Name("Tara", "Test")),
        new AccountEntity("2348", new Name("Tammy", "Test")),
        new AccountEntity("2349", new Name("Simon", "Test")),
        new AccountEntity("2350", new Name("Colin", "Test")),
        new AccountEntity("2351", new Name("Gladys", "Test")),
        new AccountEntity("2352", new Name("Greg", "Test")),
        new AccountEntity("2353", new Name("Tony", "Test")),
        new AccountEntity("2355", new Name("Arthur", "Test")),
        new AccountEntity("2356", new Name("Craig", "Test")),
        new AccountEntity("6776", new Name("Laura", "Test")),
        new AccountEntity("4534", new Name("JOSH", "TEST")),
        new AccountEntity("1234", new Name("Freya", "Test")),
        new AccountEntity("1239", new Name("Noddy", "Test")),
        new AccountEntity("1240", new Name("Archie", "Test")),
        new AccountEntity("1241", new Name("Lara", "Test")),
        new AccountEntity("1242", new Name("Tim", "Test")),
        new AccountEntity("1243", new Name("Graham", "Test")),
        new AccountEntity("1244", new Name("Tony", "Test")),
        new AccountEntity("1245", new Name("Neville", "Test")),
        new AccountEntity("1246", new Name("Jo", "Test")),
        new AccountEntity("1247", new Name("Jim", "Test")),
        new AccountEntity("1248", new Name("Pam", "Test")),
    });

    context.SaveChanges();
}
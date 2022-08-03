using Ensek.EnergyManager.Api.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("api"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/meter-reading-uploads", () =>
{
    return Results.Ok();
})
.WithName("PostMeterReadings");

// seeding
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApiContext>();
SeedAccountData(context);

app.Run();

static void SeedAccountData(ApiContext context)
{
    context.Accounts.Add(new AccountEntity("1234", new Name("Test", "Test")));

    context.SaveChanges();
}
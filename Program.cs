using Microsoft.EntityFrameworkCore;
using Api.Database;
using api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<PokemonApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.pokemontcg.io/v2/");
});

builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PokemonApiService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
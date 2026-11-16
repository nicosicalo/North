using Microsoft.EntityFrameworkCore;
using North.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ── Base de datos ─────────────────────────────────────────────────────────
// La connection string viene de:
//   - Desarrollo: dotnet user-secrets (nunca en appsettings.json)
//   - Produccion: variable de entorno CONNECTION_STRING
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? builder.Configuration["CONNECTION_STRING"]
    ?? throw new InvalidOperationException(
        "No se encontro la connection string. " +
        "En desarrollo, usa: dotnet user-secrets set ConnectionStrings:Default \"Host=...\" " +
        "En produccion, configura la variable de entorno CONNECTION_STRING.");

builder.Services.AddDbContext<AppDbContext>(options =>
    // Npgsql: EF Core provider para PostgreSQL (compatible con Supabase)
    options.UseNpgsql(connectionString));

// ── Health Check ──────────────────────────────────────────────────────────
// GET /health devuelve 200 OK si la API esta corriendo.
// En Fase 1 se agrega un check a la base de datos.
builder.Services.AddHealthChecks();

// ── Swagger ───────────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Controllers ───────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── CORS (para Angular en dev) ────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ── Middlewares ───────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DevCors");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Health check endpoint
app.MapHealthChecks("/health");

app.Run();

// Hace la clase parcial para que los tests de integracion puedan usar WebApplicationFactory
public partial class Program { }

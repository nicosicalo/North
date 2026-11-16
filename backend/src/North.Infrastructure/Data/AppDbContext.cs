using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace North.Infrastructure.Data;

/// <summary>
/// DbContext principal de la aplicacion.
/// Hereda de IdentityDbContext para incluir las tablas de ASP.NET Core Identity
/// (usuarios, roles, tokens, etc.) sin configuracion extra.
/// En Fase 1 se agregan DbSet<Category>, DbSet<Transaction>, etc.
/// </summary>
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Las configuraciones de entidades se agregan aqui en Fase 1
    }
}

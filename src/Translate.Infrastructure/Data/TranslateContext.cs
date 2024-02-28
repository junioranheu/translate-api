using Microsoft.EntityFrameworkCore;
using Translate.Domain.Entities;

namespace Translate.Infrastructure.Data;

public class TranslateContext(DbContextOptions<TranslateContext> options) : DbContext(options)
{
    public DbSet<Frase> Frases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            // relationship.DeleteBehavior = DeleteBehavior.Restrict;
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
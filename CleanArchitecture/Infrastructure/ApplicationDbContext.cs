using Application.Interfaces.Repositories;
using Application.Models.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly ConnectionStrings _connectionStrings;
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<ConnectionStrings> connectionStrings)
        : base(options)
    {
        _connectionStrings = connectionStrings.Value;
    }

    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_connectionStrings.SchemaPgSQLConnection ?? "CleanArchData");
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}

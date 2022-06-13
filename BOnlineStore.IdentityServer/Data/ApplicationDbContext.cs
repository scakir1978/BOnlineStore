using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Tenant> Tenant;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tenant>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(c => c.Users).WithOne(e => e.Tenant);
        });


        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

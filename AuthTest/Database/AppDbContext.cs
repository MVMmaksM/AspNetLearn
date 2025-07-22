using AuthTest.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;

namespace AuthTest.Database;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=admin;Password=123456",
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
    }
}
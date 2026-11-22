using EventDaysMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace EventDaysMvc.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items => Set<Item>();
}

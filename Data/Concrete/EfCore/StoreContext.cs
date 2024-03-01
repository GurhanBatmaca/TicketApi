using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class StoreContext: DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options):base(options)
    {
    }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Artor> Artors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Work> Works { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityCategory>().HasKey(e=> new {e.ActivityId,e.CategoryId});
        modelBuilder.Entity<ArtorWork>().HasKey(e=> new {e.WorkId,e.ArtorId});
        modelBuilder.Entity<TicketArtor>().HasKey(e=> new {e.TicketId,e.ArtorId});

        modelBuilder.Entity<Activity>().HasKey(e=>e.Id);
        modelBuilder.Entity<Address>().HasKey(e=>e.Id);
        modelBuilder.Entity<Artor>().HasKey(e=>e.Id);
        modelBuilder.Entity<Category>().HasKey(e=>e.Id);
        modelBuilder.Entity<Ticket>().HasKey(e=>e.Id);
        modelBuilder.Entity<Work>().HasKey(e=>e.Id);
    }
}

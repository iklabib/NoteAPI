using Microsoft.EntityFrameworkCore;


public class AppsContext : DbContext
{
    public AppsContext(DbContextOptions<AppsContext> options)
        : base(options)
    { }
    public DbSet<User> Users { get; set; }
    public DbSet<Checklist> Checklists { get; set; }
    public DbSet<ChecklistItem> ChecklistItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseLazyLoadingProxies();
    }
}
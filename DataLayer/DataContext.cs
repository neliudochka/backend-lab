using Microsoft.EntityFrameworkCore;
using Models;

namespace DataLayer;

public class DataContext: DbContext
{        
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Record> Records { get; set; }        
    public DbSet<Account> Accounts { get; set; }

    public DataContext(DbContextOptions<DataContext> options) 
        : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Records)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Records)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Record>()
            .HasOne(r => r.Category)
            .WithMany(c => c.Records)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        //account
        modelBuilder.Entity<Account>()
            .HasKey(acc => acc.Id);
        
        modelBuilder.Entity<User>()
            .HasOne<Account>(u => u.Account)
            .WithOne(r => r.User)
            .HasForeignKey<Account>(acc => acc.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
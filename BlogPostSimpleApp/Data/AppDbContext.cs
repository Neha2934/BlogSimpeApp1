using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<BlogType> BlogTypes { get; set; }
    public DbSet<PostType> PostTypes { get; set; }

    public DbSet<User> users { get; set; }
    public DbSet<Status> Statuses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    =>
   options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlogDb;Trusted_Connection = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.PostType)
            .WithMany(pt => pt.Posts)
            .HasForeignKey(p => p.PostTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Blog>()
            .HasOne(p => p.BlogType)
            .WithMany(pt => pt.Blogs)
            .HasForeignKey(p => p.BlogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()
        .HasOne(p => p.Blog)
        .WithMany(b => b.Posts)
        .HasForeignKey(p => p.BlogId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);

            entity.Property(u => u.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.PhoneNumber)
                  .HasMaxLength(15);
        });

        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasOne(b => b.Status)
                  .WithMany(s => s.Blogs)
                  .HasForeignKey(b => b.StatusId);
        });
    }
}
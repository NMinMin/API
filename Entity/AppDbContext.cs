using Microsoft.EntityFrameworkCore;

namespace API.Entity;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
  : base(options) { }

  public DbSet<Student> Students { get; set; }
  public DbSet<Class> Classes { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>() //Cấu hình Entity Class
            .HasMany(c => c.lst_student)//1 class có nhiều student
            .WithOne(s => s._class!)//1 student chỉ có 1 class
            .HasForeignKey(s => s.class_id)//Khóa ngoại
            .OnDelete(DeleteBehavior.Cascade);//Khi xóa class thì xóa luôn student thuộc class đó
    }
}
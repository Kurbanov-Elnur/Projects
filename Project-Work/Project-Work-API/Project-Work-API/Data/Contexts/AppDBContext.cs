using Microsoft.EntityFrameworkCore;
using Project_Work_API.Data.Models.DBModels;

namespace Project_Work_API.Data.Contexts;

public class AppDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userEntity = modelBuilder.Entity<User>();
        var departmentEntity = modelBuilder.Entity<Department>();
        var facultyEntity = modelBuilder.Entity<Faculty>();
        var groupEntity = modelBuilder.Entity<Group>();
        var studentEntity = modelBuilder.Entity<Student>();
        var teacherEntity = modelBuilder.Entity<Teacher>();

        userEntity.HasKey(u => u.Id);

        userEntity.Property(u => u.Id)
            .IsRequired();

        userEntity.Property(u => u.Name)
            .IsRequired();

        userEntity.Property(u => u.Surname)
            .IsRequired();

        userEntity.Property(u => u.Password)
            .IsRequired();

        userEntity.Property(u => u.Email)
            .IsRequired();
        userEntity.HasIndex(u => u.Email)
            .IsUnique();

        userEntity.Property(u => u.Role)
            .IsRequired();

        userEntity.Property(u => u.RefreshToken);

        userEntity.Property(u => u.RefreshTokenExpiryTime);

        userEntity.HasOne(u => u.Teacher)
            .WithOne(t => t.User)
            .HasForeignKey<Teacher>(t => t.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        userEntity.HasOne(u => u.Student)
            .WithOne(s => s.User)
            .HasForeignKey<Student>(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        departmentEntity.HasKey(d => d.Id);

        departmentEntity.Property(d => d.Id)
            .IsRequired();

        departmentEntity.Property(d => d.Name)
            .IsRequired();
        departmentEntity.HasIndex(d => d.Name)
            .IsUnique();

        departmentEntity.HasOne(d => d.Faculty)
            .WithMany(f => f.Departments)
            .HasForeignKey(d => d.FacultyId)
            .OnDelete(DeleteBehavior.SetNull);

        departmentEntity.HasMany(d => d.Groups)
            .WithOne(g => g.Department)
            .HasForeignKey(g => g.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        facultyEntity.HasKey(f => f.Id);

        facultyEntity.Property(f => f.Id)
            .IsRequired();

        facultyEntity.Property(f => f.Name)
            .IsRequired();
        facultyEntity.HasIndex(f => f.Name)
            .IsUnique();

        facultyEntity.HasMany(f => f.Departments)
            .WithOne(d => d.Faculty)
            .HasForeignKey(d => d.FacultyId)
            .OnDelete(DeleteBehavior.SetNull);

        facultyEntity.HasMany(f => f.Teachers)
            .WithOne(t => t.Faculty)
            .HasForeignKey(t => t.FacultyId)
            .OnDelete(DeleteBehavior.SetNull);

        groupEntity.HasKey(g => g.Id);

        groupEntity.Property(g => g.Id)
            .IsRequired();

        groupEntity.Property(g => g.Name)
            .IsRequired();
        groupEntity.HasIndex(g => g.Name)
            .IsUnique();

        groupEntity.HasOne(g => g.Teacher)
            .WithMany(t => t.Groups)
            .HasForeignKey(g => g.TeacherId)
            .OnDelete(DeleteBehavior.SetNull);  

        groupEntity.HasOne(g => g.Department)
            .WithMany(d => d.Groups)
            .HasForeignKey(g => g.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        groupEntity.HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.SetNull);

        studentEntity.HasKey(s => s.Id);

        studentEntity.Property(s => s.Id)
            .IsRequired();

        studentEntity.HasOne(s => s.User)
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        studentEntity.HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.SetNull);

        teacherEntity.HasKey(t => t.Id);

        teacherEntity.Property(t => t.Id)
            .IsRequired();

        teacherEntity.HasOne(t => t.User)
            .WithOne(u => u.Teacher)
            .HasForeignKey<Teacher>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        teacherEntity.HasOne(t => t.Faculty)
            .WithMany(f => f.Teachers)
            .HasForeignKey(t => t.FacultyId)
            .OnDelete(DeleteBehavior.SetNull);

        teacherEntity.HasMany(t => t.Groups)
            .WithOne(g => g.Teacher)
            .HasForeignKey(g => g.TeacherId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
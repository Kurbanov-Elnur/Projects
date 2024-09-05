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

        userEntity.HasMany(u => u.Teachers)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        userEntity.HasMany(u => u.Students)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .IsRequired();

        departmentEntity.HasKey(d => d.Id);

        departmentEntity.Property(d => d.Id)
            .IsRequired();

        departmentEntity.Property(d => d.Name)
            .IsRequired();

        departmentEntity.HasOne(d => d.Faculty)
            .WithMany(f => f.Departments)
            .HasForeignKey(d => d.FacultyId)
            .IsRequired();

        departmentEntity.HasMany(d => d.Groups)
            .WithOne(g => g.Department)
            .HasForeignKey(g => g.DepartmentId);

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
            .IsRequired();

        facultyEntity.HasMany(f => f.Teachers)
            .WithOne(t => t.Faculty)
            .HasForeignKey(t => t.FacultyId)
            .IsRequired();

        groupEntity.HasKey(g => g.Id);

        groupEntity.Property(g => g.Id)
            .IsRequired();

        groupEntity.Property(g => g.Name)
            .IsRequired();

        groupEntity.HasOne(g => g.Teacher)
            .WithMany(t => t.Groups)
            .HasForeignKey(g => g.TeacherId)
            .IsRequired();

        groupEntity.HasOne(g => g.Department)
            .WithMany(d => d.Groups)
            .HasForeignKey(g => g.DepartmentId)
            .IsRequired();

        groupEntity.HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId)
            .IsRequired();

        studentEntity.HasKey(s => s.Id);

        studentEntity.Property(s => s.Id)
            .IsRequired();

        studentEntity.HasOne(s => s.User)
            .WithMany(u => u.Students)
            .HasForeignKey(s => s.UserId)
            .IsRequired();

        studentEntity.HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId)
            .IsRequired();

        teacherEntity.HasKey(t => t.Id);

        teacherEntity.Property(t => t.Id)
            .IsRequired();

        teacherEntity.HasOne(t => t.User)
            .WithMany(u => u.Teachers)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        teacherEntity.HasOne(t => t.Faculty)
            .WithMany(f => f.Teachers)
            .HasForeignKey(t => t.FacultyId)
            .IsRequired();

        teacherEntity.HasMany(t => t.Groups)
            .WithOne(g => g.Teacher)
            .HasForeignKey(g => g.TeacherId)
            .IsRequired();
    }
}
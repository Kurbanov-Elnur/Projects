using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Models;

namespace Trendyol;

class AppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Movies; Integrated Security=True; Trust Server Certificate = True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userEntity = modelBuilder.Entity<User>();
        var orderEntity = modelBuilder.Entity<Order>();

        userEntity.HasKey(x => x.Id);
        userEntity
            .Property(x => x.email)
            .IsRequired();
        userEntity
            .Property(x => x.password)
            .IsRequired();

        orderEntity.HasKey(x => x.Id);
        orderEntity.Property(x => x.name)
            .IsRequired()
            .HasMaxLength(30);

        orderEntity
            .HasMany(x => x.users)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

    }
}
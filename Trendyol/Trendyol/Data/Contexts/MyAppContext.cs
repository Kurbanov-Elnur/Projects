using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Models;

namespace Trendyol.Data.Contexts;

class MyAppContext : DbContext
{
    /*
     * Super Admin - mursalzadeh99@gmail.com
     * Password - Elnur123
     * 
     * Admin - atillaristam@gmail.com
     * Password - Atilla123
     * 
     * User - kanan-memmedli-09@mail.ru
     * Password - Kanan123
     */

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(builder.GetConnectionString("Default"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var order = modelBuilder.Entity<Order>();
        var product = modelBuilder.Entity<Product>();
        var user = modelBuilder.Entity<User>();
        var warehouse = modelBuilder.Entity<Warehouse>();

        order
            .HasKey(o => o.Id);
        
        order
            .Property(o => o.TrackID)
            .IsRequired();

        order
            .Property(o => o.PurchaseDate)
            .IsRequired();

        order
            .Property(o => o.Count)
            .IsRequired();

        order
            .Property(o => o.Status)
            .IsRequired();

        product
            .HasKey(p => p.Id);

        product
            .Property(o => o.Name)
            .IsRequired();

        product
            .Property(o => o.Description)
            .IsRequired();

        product
            .Property(o => o.Brand)
            .IsRequired();

        product
            .Property(o => o.Price)
            .IsRequired();

        product
            .Property(o => o.Image)
            .IsRequired();

        product
            .HasMany(o => o.Orders)
            .WithOne(o => o.Product)
            .HasForeignKey(o => o.ProductID);

        product
            .HasMany(w => w.Warehouse)
            .WithOne(w => w.Product)
            .HasForeignKey(w => w.ProductID);

        user
            .HasKey(u => u.Id);

        user
            .Property(u => u.Name)
            .IsRequired();

        user
            .Property(u => u.Surname)
            .IsRequired();

        user
            .Property(u => u.Email)
            .IsRequired();

        user
            .Property(u => u.Password)
            .IsRequired();

        user
            .Property(u => u.Role)
            .IsRequired();

        user
            .Property(u => u.Image)
            .IsRequired();

        user
            .HasMany(u => u.Orders)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserID);

        warehouse
            .HasKey(w => w.Id);

        warehouse
            .Property(w => w.Count)
            .IsRequired();
    }
}
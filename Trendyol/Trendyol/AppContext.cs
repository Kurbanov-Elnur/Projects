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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Trendyol; Integrated Security=True; Trust Server Certificate = True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userEntity = modelBuilder.Entity<User>();

        userEntity
            .HasKey(x => x.Id);

        userEntity
            .Property(x => x.Name)
            .IsRequired();
        
        userEntity
            .Property(x => x.Surname)
            .IsRequired();

        userEntity
            .Property(x => x.Email)
            .IsRequired();

        userEntity
            .Property(x => x.Password)
            .IsRequired();
    }
}
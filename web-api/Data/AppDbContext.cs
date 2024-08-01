using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api.Controllers;
using web_api.Models;

namespace web_api.Data
{
    public class AppDbContext : DbContext 
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

       public DbSet<User> Users {get; set;}
       public DbSet<Run> Runs {get; set;}
       public DbSet<Weapons> Weapons {get; set;}
       public DbSet<Tools> Tools {get; set;}
       public DbSet<RunWeapon> RunWeapons {get; set;}
       public DbSet<RunTool> RunTools {get; set;}

        /*Other Methods for later*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, Username = "tafry", Password = "123456"}
            );

            modelBuilder.Entity<Run>()
                .HasMany(r => r.RunWeapons)
                .WithOne(rw => rw.Run)
                .HasForeignKey(rw => rw.RunId);

            modelBuilder.Entity<Run>()
                .HasMany(r => r.RunTools)
                .WithOne(rt => rt.Run)
                .HasForeignKey(rt => rt.RunId);

            modelBuilder.Entity<RunWeapon>()
                .HasOne(rw => rw.Weapon)
                .WithMany()
                .HasForeignKey(rw => rw.WeaponId);

            modelBuilder.Entity<RunTool>()
                .HasOne(rt => rt.Tool)
                .WithMany()
                .HasForeignKey(rt => rt.ToolId);

        }
    }
}
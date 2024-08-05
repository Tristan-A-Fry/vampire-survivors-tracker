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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<Maps> Map {get; set;}
        public DbSet<Weapons> Weapons { get; set; }
        public DbSet<Tools> Tools { get; set; }
        public DbSet<RunWeapon> RunWeapons { get; set; }
        public DbSet<RunTool> RunTools { get; set; }
        public DbSet<Characters> Characters {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seeding basic data in 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "tafry", Password = "123456", Role = "Admin"},
                new User{ Id = 2, Username = "bill", Password = "123456", Role = "Admin"}
            );
            modelBuilder.Entity<Weapons>().HasData(
                new Weapons { Id = 1, Name = "Sword" }
            );
            modelBuilder.Entity<Tools>().HasData(
                new Tools { Id = 1, Name = "CDR" }
            );
            modelBuilder.Entity<Maps>().HasData(
                new Maps { Id = 1, Name = "Forrest" },
                new Maps { Id = 2, Name = "Library" }
            );
            modelBuilder.Entity<Characters>().HasData(
                new Characters { Id = 1, Name = "char 1" }
            );

//Esatblishing relationships
            modelBuilder.Entity<Run>()
                .HasMany(r => r.RunWeapons)
                .WithOne(rw => rw.Run)
                .HasForeignKey(rw => rw.RunId);

            modelBuilder.Entity<Run>()
                .HasMany(r => r.RunTools)
                .WithOne(rt => rt.Run)
                .HasForeignKey(rt => rt.RunId);

            modelBuilder.Entity<Run>()
                .HasOne(r => r.Map)
                .WithMany(m => m.Runs)
                .HasForeignKey(r => r.MapId);

            modelBuilder.Entity<Run>()
                .HasOne(r => r.User)
                .WithMany(r => r.Runs)
                .HasForeignKey(r => r.UserId);

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
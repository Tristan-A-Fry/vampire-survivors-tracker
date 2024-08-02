﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_api.Data;

#nullable disable

namespace web_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240802002452_maps,Weapons,Tools")]
    partial class mapsWeaponsTools
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("web_api.Models.Characters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("web_api.Models.Maps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Maps");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Forrest"
                        });
                });

            modelBuilder.Entity("web_api.Models.Run", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("GoldEarned")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MapId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("MapId");

                    b.ToTable("Runs");
                });

            modelBuilder.Entity("web_api.Models.RunTool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RunId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RunId");

                    b.HasIndex("ToolId");

                    b.ToTable("RunTools");
                });

            modelBuilder.Entity("web_api.Models.RunWeapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEvolved")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RunId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeaponId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RunId");

                    b.HasIndex("WeaponId");

                    b.ToTable("RunWeapons");
                });

            modelBuilder.Entity("web_api.Models.Tools", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tools");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "CDR"
                        });
                });

            modelBuilder.Entity("web_api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "123456",
                            Username = "tafry"
                        });
                });

            modelBuilder.Entity("web_api.Models.Weapons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Weapons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sword"
                        });
                });

            modelBuilder.Entity("web_api.Models.Run", b =>
                {
                    b.HasOne("web_api.Models.Characters", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("web_api.Models.Maps", "Map")
                        .WithMany()
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Map");
                });

            modelBuilder.Entity("web_api.Models.RunTool", b =>
                {
                    b.HasOne("web_api.Models.Run", "Run")
                        .WithMany("RunTools")
                        .HasForeignKey("RunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("web_api.Models.Tools", "Tool")
                        .WithMany()
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Run");

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("web_api.Models.RunWeapon", b =>
                {
                    b.HasOne("web_api.Models.Run", "Run")
                        .WithMany("RunWeapons")
                        .HasForeignKey("RunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("web_api.Models.Weapons", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Run");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("web_api.Models.Run", b =>
                {
                    b.Navigation("RunTools");

                    b.Navigation("RunWeapons");
                });
#pragma warning restore 612, 618
        }
    }
}

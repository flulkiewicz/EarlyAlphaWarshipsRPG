﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarshipsRPGBeta.Data;

#nullable disable

namespace WarshipsRPGAlpha.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230505161429_SpecialWaeponry")]
    partial class SpecialWaeponry
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShipSpecialWaepon", b =>
                {
                    b.Property<int>("ShipsId")
                        .HasColumnType("int");

                    b.Property<int>("SpecialWaeponsId")
                        .HasColumnType("int");

                    b.HasKey("ShipsId", "SpecialWaeponsId");

                    b.HasIndex("SpecialWaeponsId");

                    b.ToTable("ShipSpecialWaepon");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.MainGun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShipId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShipId")
                        .IsUnique();

                    b.ToTable("MainGuns");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.Ship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Armor")
                        .HasColumnType("int");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("Crew")
                        .HasColumnType("int");

                    b.Property<int>("Faction")
                        .HasColumnType("int");

                    b.Property<int>("FirePower")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.SpecialWaepon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SpecialWaepons");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShipSpecialWaepon", b =>
                {
                    b.HasOne("WarshipsRPGAlpha.Models.Ship", null)
                        .WithMany()
                        .HasForeignKey("ShipsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarshipsRPGAlpha.Models.SpecialWaepon", null)
                        .WithMany()
                        .HasForeignKey("SpecialWaeponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.MainGun", b =>
                {
                    b.HasOne("WarshipsRPGAlpha.Models.Ship", "Ship")
                        .WithOne("MainGun")
                        .HasForeignKey("WarshipsRPGAlpha.Models.MainGun", "ShipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ship");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.Ship", b =>
                {
                    b.HasOne("WarshipsRPGAlpha.Models.User", "User")
                        .WithMany("Ships")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.Ship", b =>
                {
                    b.Navigation("MainGun");
                });

            modelBuilder.Entity("WarshipsRPGAlpha.Models.User", b =>
                {
                    b.Navigation("Ships");
                });
#pragma warning restore 612, 618
        }
    }
}

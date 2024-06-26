﻿// <auto-generated />
using System;
using Game_Design_DB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Game_Design_DB.Migrations
{
    [DbContext(typeof(Game_Design_DBContext))]
    [Migration("20240329142151_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GamePerson", b =>
                {
                    b.Property<int>("GamesID")
                        .HasColumnType("integer");

                    b.Property<int>("PeopleID")
                        .HasColumnType("integer");

                    b.HasKey("GamesID", "PeopleID");

                    b.HasIndex("PeopleID");

                    b.ToTable("GamePerson");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Developer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Engine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int?>("DeveloperID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DeveloperID");

                    b.ToTable("Engine");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("DeveloperID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ResourceID")
                        .HasColumnType("integer");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DeveloperID");

                    b.HasIndex("ResourceID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int?>("DeveloperID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DeveloperID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Game_Design_DB.Models.PersonalWebsite", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("PersonalWebsite");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Resource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("PersonResource", b =>
                {
                    b.Property<int>("AuthorsID")
                        .HasColumnType("integer");

                    b.Property<int>("ResourcesID")
                        .HasColumnType("integer");

                    b.HasKey("AuthorsID", "ResourcesID");

                    b.HasIndex("ResourcesID");

                    b.ToTable("PersonResource");
                });

            modelBuilder.Entity("GamePerson", b =>
                {
                    b.HasOne("Game_Design_DB.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Game_Design_DB.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PeopleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game_Design_DB.Models.Engine", b =>
                {
                    b.HasOne("Game_Design_DB.Models.Developer", "Developer")
                        .WithMany("Engines")
                        .HasForeignKey("DeveloperID");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Game", b =>
                {
                    b.HasOne("Game_Design_DB.Models.Developer", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Game_Design_DB.Models.Resource", null)
                        .WithMany("RelatedGames")
                        .HasForeignKey("ResourceID");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Person", b =>
                {
                    b.HasOne("Game_Design_DB.Models.Developer", null)
                        .WithMany("People")
                        .HasForeignKey("DeveloperID");
                });

            modelBuilder.Entity("Game_Design_DB.Models.PersonalWebsite", b =>
                {
                    b.HasOne("Game_Design_DB.Models.Person", "Person")
                        .WithMany("Websites")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("PersonResource", b =>
                {
                    b.HasOne("Game_Design_DB.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("AuthorsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Game_Design_DB.Models.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourcesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game_Design_DB.Models.Developer", b =>
                {
                    b.Navigation("Engines");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Person", b =>
                {
                    b.Navigation("Websites");
                });

            modelBuilder.Entity("Game_Design_DB.Models.Resource", b =>
                {
                    b.Navigation("RelatedGames");
                });
#pragma warning restore 612, 618
        }
    }
}

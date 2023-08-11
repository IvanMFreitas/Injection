﻿// <auto-generated />
using System;
using Injection.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Injection.Data.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Injection.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("Decimal(5,2)")
                        .HasColumnName("Total");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("257ffa96-574f-4337-b22c-82db6eb705d4"),
                            CreatedAt = new DateTime(2023, 8, 10, 20, 57, 59, 779, DateTimeKind.Local).AddTicks(5627),
                            PersonId = new Guid("4d6878a8-0ef1-4646-9f03-6a4c791a3a07"),
                            ProductId = new Guid("562e69fe-b889-4dfa-a2b8-e9cc293fa895"),
                            Qty = 1,
                            Total = 2m
                        });
                });

            modelBuilder.Entity("Injection.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Person", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4d6878a8-0ef1-4646-9f03-6a4c791a3a07"),
                            CreatedAt = new DateTime(2023, 8, 10, 20, 57, 59, 779, DateTimeKind.Local).AddTicks(5199),
                            Email = "person1@api.com",
                            IsAdmin = true,
                            Name = "Person 1"
                        },
                        new
                        {
                            Id = new Guid("d741d707-730a-420e-a569-ec9854cbfe72"),
                            CreatedAt = new DateTime(2023, 8, 10, 20, 57, 59, 779, DateTimeKind.Local).AddTicks(5236),
                            Email = "person2@api.com",
                            IsAdmin = false,
                            Name = "Person 2"
                        });
                });

            modelBuilder.Entity("Injection.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("Decimal(5,2)")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("562e69fe-b889-4dfa-a2b8-e9cc293fa895"),
                            CreatedAt = new DateTime(2023, 8, 10, 20, 57, 59, 779, DateTimeKind.Local).AddTicks(5588),
                            Name = "Product 1",
                            Price = 2m
                        },
                        new
                        {
                            Id = new Guid("22543fd3-ff13-4a1f-a215-f7399fd9fca6"),
                            CreatedAt = new DateTime(2023, 8, 10, 20, 57, 59, 779, DateTimeKind.Local).AddTicks(5593),
                            Name = "Product 2",
                            Price = 5m
                        });
                });

            modelBuilder.Entity("Injection.Entities.Order", b =>
                {
                    b.HasOne("Injection.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Injection.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}

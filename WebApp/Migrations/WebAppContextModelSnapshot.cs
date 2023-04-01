﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

namespace WebApp.Migrations
{
    [DbContext(typeof(WebAppContext))]
    partial class WebAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebApp.Models.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("WebApp.Models.SalesRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<long?>("SellerId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("SalesRecord");
                });

            modelBuilder.Entity("WebApp.Models.Seller", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BaseSalary");

                    b.Property<DateTime>("BirthDate");

                    b.Property<long>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("WebApp.Models.SalesRecord", b =>
                {
                    b.HasOne("WebApp.Models.Seller", "Seller")
                        .WithMany("Sales")
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("WebApp.Models.Seller", b =>
                {
                    b.HasOne("WebApp.Models.Department", "Department")
                        .WithMany("Sellers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

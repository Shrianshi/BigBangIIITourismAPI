﻿// <auto-generated />
using System;
using BigBangIII_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BigBangIII_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230804175140_Migrations2")]
    partial class Migrations2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BigBangIII_Api.Models.Agent", b =>
                {
                    b.Property<int>("Agent_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Agent_id"));

                    b.Property<string>("AgentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Agent_id");

                    b.HasIndex("UserId");

                    b.ToTable("agents");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Billing", b =>
                {
                    b.Property<int>("Bill_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Bill_id"));

                    b.Property<int?>("Book_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Tax")
                        .HasColumnType("int");

                    b.Property<int?>("Total_cost")
                        .HasColumnType("int");

                    b.HasKey("Bill_id");

                    b.HasIndex("Book_Id");

                    b.ToTable("billings");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Bookings", b =>
                {
                    b.Property<int>("Book_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Book_Id"));

                    b.Property<string>("Booking_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("P_id")
                        .HasColumnType("int");

                    b.Property<string>("Tname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Book_Id");

                    b.HasIndex("P_id");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("feedbacks");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Images", b =>
                {
                    b.Property<int>("Image_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Image_Id"));

                    b.Property<string>("IName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Image_Id");

                    b.ToTable("images");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Packages", b =>
                {
                    b.Property<int>("P_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("P_id"));

                    b.Property<string>("Acc_details")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Desc")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Food_Details")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("P_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Pricing")
                        .HasColumnType("int");

                    b.HasKey("P_id");

                    b.ToTable("packages");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Agent", b =>
                {
                    b.HasOne("BigBangIII_Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Billing", b =>
                {
                    b.HasOne("BigBangIII_Api.Models.Bookings", "Bookings")
                        .WithMany()
                        .HasForeignKey("Book_Id");

                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Bookings", b =>
                {
                    b.HasOne("BigBangIII_Api.Models.Packages", "Packages")
                        .WithMany()
                        .HasForeignKey("P_id");

                    b.Navigation("Packages");
                });

            modelBuilder.Entity("BigBangIII_Api.Models.Feedback", b =>
                {
                    b.HasOne("BigBangIII_Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

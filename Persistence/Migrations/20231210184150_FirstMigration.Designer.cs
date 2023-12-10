﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231210184150_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStatusId")
                        .HasColumnType("int");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentStatusId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entities.PaymentData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CardBrand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastFourDigits")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("PaymentsData");
                });

            modelBuilder.Entity("Domain.Entities.PaymentError", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ErrorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ErrorId");

                    b.HasIndex("PaymentId");

                    b.ToTable("PaymentsErros");
                });

            modelBuilder.Entity("Domain.Entities.PaymentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PaymentStatuses");
                });

            modelBuilder.Entity("Domain.Entities.Refund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Refunds");
                });

            modelBuilder.Entity("Domain.Entities.RefundError", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ErrorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("RefundId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ErrorId");

                    b.HasIndex("RefundId");

                    b.ToTable("RefundsErrors");
                });

            modelBuilder.Entity("Domain.Entities.RefundStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("RefundStatuses");
                });

            modelBuilder.Entity("Domain.Entities.Terminal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Terminals");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasOne("Domain.Entities.PaymentStatus", "PaymentStatus")
                        .WithMany()
                        .HasForeignKey("PaymentStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Terminal", "Terminal")
                        .WithMany()
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentStatus");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("Domain.Entities.PaymentData", b =>
                {
                    b.HasOne("Domain.Entities.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("Domain.Entities.PaymentError", b =>
                {
                    b.HasOne("Domain.Entities.Error", "Error")
                        .WithMany()
                        .HasForeignKey("ErrorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Error");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("Domain.Entities.Refund", b =>
                {
                    b.HasOne("Domain.Entities.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("Domain.Entities.RefundError", b =>
                {
                    b.HasOne("Domain.Entities.Error", "Error")
                        .WithMany()
                        .HasForeignKey("ErrorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Refund", "Refund")
                        .WithMany()
                        .HasForeignKey("RefundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Error");

                    b.Navigation("Refund");
                });
#pragma warning restore 612, 618
        }
    }
}

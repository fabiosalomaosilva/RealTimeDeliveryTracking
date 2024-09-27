﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Postgres;

#nullable disable

namespace Postgres.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20240927204217_CreateDeliverytables")]
    partial class CreateDeliverytables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Domain.DeliveryHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.ToTable("DeliveryHistory");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.DeliveryHistory", b =>
                {
                    b.HasOne("Domain.Delivery", "Delivery")
                        .WithMany("History")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Delivery");
                });

            modelBuilder.Entity("Domain.Delivery", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using barboek.Data;

#nullable disable

namespace barboek.Data.Migrations
{
    [DbContext(typeof(DataStore))]
    [Migration("20231122223516_PriceUpdate")]
    partial class PriceUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("barboek.Interface.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("barboek.Interface.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("barboek.Interface.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountOrderedId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountOrderedId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("barboek.Interface.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("barboek.Interface.Models.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("PriceTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PriceTypeId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("barboek.Interface.Models.PriceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PriceTypes");
                });

            modelBuilder.Entity("barboek.Interface.Models.Order", b =>
                {
                    b.HasOne("barboek.Interface.Models.Account", "AccountOrdered")
                        .WithMany()
                        .HasForeignKey("AccountOrderedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountOrdered");
                });

            modelBuilder.Entity("barboek.Interface.Models.OrderItem", b =>
                {
                    b.HasOne("barboek.Interface.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("barboek.Interface.Models.Order", null)
                        .WithMany("OrderedItems")
                        .HasForeignKey("OrderId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("barboek.Interface.Models.Price", b =>
                {
                    b.HasOne("barboek.Interface.Models.Item", null)
                        .WithMany("Prices")
                        .HasForeignKey("ItemId");

                    b.HasOne("barboek.Interface.Models.PriceType", "PriceType")
                        .WithMany()
                        .HasForeignKey("PriceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceType");
                });

            modelBuilder.Entity("barboek.Interface.Models.Item", b =>
                {
                    b.Navigation("Prices");
                });

            modelBuilder.Entity("barboek.Interface.Models.Order", b =>
                {
                    b.Navigation("OrderedItems");
                });
#pragma warning restore 612, 618
        }
    }
}

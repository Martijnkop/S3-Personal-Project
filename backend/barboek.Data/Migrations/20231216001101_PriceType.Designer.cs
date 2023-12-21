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
    [Migration("20231216001101_PriceType")]
    partial class PriceType
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

            modelBuilder.Entity("barboek.Interface.Models.Database.DbPriceType", b =>
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

            modelBuilder.Entity("barboek.Interface.Models.Database.DbTaxType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BeginTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Percentage")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("TaxTypes");
                });

            modelBuilder.Entity("barboek.Interface.Models.Database.DbUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("barboek.Interface.Models.OldItem", b =>
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

            modelBuilder.Entity("barboek.Interface.Models.OldOrder", b =>
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

            modelBuilder.Entity("barboek.Interface.Models.OldPrice", b =>
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

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("OldItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OldPriceTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OldItemId");

                    b.HasIndex("OldPriceTypeId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("barboek.Interface.Models.OldPriceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OldPriceTypes");
                });

            modelBuilder.Entity("barboek.Interface.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OldOrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OldOrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("barboek.Interface.Models.OldOrder", b =>
                {
                    b.HasOne("barboek.Interface.Models.Account", "AccountOrdered")
                        .WithMany()
                        .HasForeignKey("AccountOrderedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountOrdered");
                });

            modelBuilder.Entity("barboek.Interface.Models.OldPrice", b =>
                {
                    b.HasOne("barboek.Interface.Models.OldItem", null)
                        .WithMany("Prices")
                        .HasForeignKey("OldItemId");

                    b.HasOne("barboek.Interface.Models.OldPriceType", "OldPriceType")
                        .WithMany()
                        .HasForeignKey("OldPriceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OldPriceType");
                });

            modelBuilder.Entity("barboek.Interface.Models.OrderItem", b =>
                {
                    b.HasOne("barboek.Interface.Models.OldItem", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("barboek.Interface.Models.OldOrder", null)
                        .WithMany("OrderedItems")
                        .HasForeignKey("OldOrderId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("barboek.Interface.Models.OldItem", b =>
                {
                    b.Navigation("Prices");
                });

            modelBuilder.Entity("barboek.Interface.Models.OldOrder", b =>
                {
                    b.Navigation("OrderedItems");
                });
#pragma warning restore 612, 618
        }
    }
}

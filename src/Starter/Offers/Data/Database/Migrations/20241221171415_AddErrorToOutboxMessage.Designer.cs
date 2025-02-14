﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Starter.Offers.Data.Database;

#nullable disable

namespace Starter.Offers.Data.Database.Migrations
{
    [DbContext(typeof(OffersPersistence))]
    [Migration("20241221171415_AddErrorToOutboxMessage")]
    partial class AddErrorToOutboxMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Offers")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Starter.Common.EventualConsistency.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("ExecutedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<DateTimeOffset>("SavedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExecutedOn");

                    b.ToTable("OutboxMessage", "Offers");
                });

            modelBuilder.Entity("Starter.Offers.Data.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("OfferedFromDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("OfferedFromTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("PreparedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Offers", "Offers");
                });
#pragma warning restore 612, 618
        }
    }
}

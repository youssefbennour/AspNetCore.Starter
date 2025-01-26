﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Starter.Contracts.Data.Database;

#nullable disable

namespace Starter.Contracts.Data.Database.Migrations
{
    [DbContext(typeof(ContractsPersistence))]
    [Migration("20241221171451_AddErrorToOutboxMessage")]
    partial class AddErrorToOutboxMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Contracts")
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

                    b.ToTable("OutboxMessage", "Contracts");
                });

            modelBuilder.Entity("Starter.Contracts.Data.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTimeOffset?>("ExpiringAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("PreparedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("SignedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Contracts", "Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}

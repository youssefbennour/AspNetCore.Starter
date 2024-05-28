﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Starter.Offers.Data.Database;

#nullable disable

namespace SuperSimpleArchitecture.Fitnet.Migrations.OffersPersistenceMigrations;

using System.Diagnostics.CodeAnalysis;

[DbContext(typeof(OffersPersistence))]
[ExcludeFromCodeCoverage]
partial class OffersPersistenceModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasDefaultSchema("Offers")
            .HasAnnotation("ProductVersion", "7.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("SuperSimpleArchitecture.Fitnet.Offers.Data.Offer", b =>
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
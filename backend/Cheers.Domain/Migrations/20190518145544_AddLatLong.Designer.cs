﻿// <auto-generated />
using System;
using Cheers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Cheers.Domain.Migrations
{
    [DbContext(typeof(CheersDbContext))]
    [Migration("20190518145544_AddLatLong")]
    partial class AddLatLong
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Cheers.Domain.Entities.Rating", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<int>("Score");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<long?>("VenueId");

                    b.HasKey("Id");

                    b.HasIndex("VenueId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Cheers.Domain.Entities.Venue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("Cheers.Domain.Entities.Rating", b =>
                {
                    b.HasOne("Cheers.Domain.Entities.Venue", "Venue")
                        .WithMany()
                        .HasForeignKey("VenueId");
                });
#pragma warning restore 612, 618
        }
    }
}

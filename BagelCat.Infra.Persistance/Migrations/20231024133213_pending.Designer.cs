﻿// <auto-generated />
using System;
using BagelCat.Infra.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BagelCat.Infra.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231024133213_pending")]
    partial class pending
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BagelCat.Infra.Persistance.Models.IntegrationEventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IntegrationEventOutbox");
                });

            modelBuilder.Entity("BagelCat.Infra.Persistance.Models.WeatherEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RecordDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TemperatureInCelcius")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WeatherEntities");
                });
#pragma warning restore 612, 618
        }
    }
}

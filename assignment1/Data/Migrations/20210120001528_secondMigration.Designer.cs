﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using assignment1.Data;

namespace assignment1.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210120001528_secondMigration")]
    partial class secondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("assignment1.Entities.CovidData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.Property<int>("numconf")
                        .HasColumnType("INTEGER");

                    b.Property<int>("numdeaths")
                        .HasColumnType("INTEGER");

                    b.Property<int>("numprob")
                        .HasColumnType("INTEGER");

                    b.Property<int>("numtoday")
                        .HasColumnType("INTEGER");

                    b.Property<int>("numtotal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("prname")
                        .HasColumnType("TEXT");

                    b.Property<string>("prnameFR")
                        .HasColumnType("TEXT");

                    b.Property<int>("pruid")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ratetotal")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("DailyCovidData");
                });
#pragma warning restore 612, 618
        }
    }
}

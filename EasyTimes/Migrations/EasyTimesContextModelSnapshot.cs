﻿// <auto-generated />
using System;
using EasyTimes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyTimes.Migrations
{
    [DbContext(typeof(EasyTimesContext))]
    partial class EasyTimesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyTimes.Models.Charger", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End_");

                    b.Property<double>("Hours_");

                    b.Property<double>("OnTheRanch");

                    b.Property<DateTime>("Start_");

                    b.HasKey("id");

                    b.ToTable("Charger");
                });

            modelBuilder.Entity("EasyTimes.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ");

                    b.Property<string>("MainContactEmail");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("EasyTimes.Models.LittleTask", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Json");

                    b.Property<int>("ServiceOrderID");

                    b.HasKey("id");

                    b.ToTable("LittleTask");
                });

            modelBuilder.Entity("EasyTimes.Models.Owner", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Agency");

                    b.Property<string>("Bank");

                    b.Property<string>("CurrentAccount");

                    b.Property<string>("Email");

                    b.Property<double>("GasPrice");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<double>("PricePerHour");

                    b.HasKey("id");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("EasyTimes.Models.ServiceOrder", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AmountOfHours");

                    b.Property<int>("ClientID");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("SerialCode");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("id");

                    b.ToTable("ServiceOrder");
                });
#pragma warning restore 612, 618
        }
    }
}

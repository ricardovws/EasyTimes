﻿// <auto-generated />
using System;
using EasyTimes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EasyTimes.Models.Charger", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End_");

                    b.Property<double>("Hours_");

                    b.Property<double>("OnTheRanch");

                    b.Property<int>("ServiceOrderID");

                    b.Property<DateTime>("Start_");

                    b.HasKey("id");

                    b.ToTable("Charger");
                });

            modelBuilder.Entity("EasyTimes.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BetweenBoth");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Date_string");

                    b.Property<DateTime>("End");

                    b.Property<string>("End_string");

                    b.Property<bool>("MealTicket");

                    b.Property<bool>("Overtime");

                    b.Property<double>("OvertimeValue");

                    b.Property<int>("ServiceOrderID");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Start_string");

                    b.Property<double>("kM");

                    b.HasKey("id");

                    b.ToTable("LittleTask");
                });

            modelBuilder.Entity("EasyTimes.Models.Owner", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Agency");

                    b.Property<string>("Bank");

                    b.Property<string>("CurrentAccount");

                    b.Property<string>("Email");

                    b.Property<double>("GasPrice");

                    b.Property<double>("MealTicket");

                    b.Property<string>("Name");

                    b.Property<double>("NormalTime");

                    b.Property<double>("OvertimeProfitRate");

                    b.Property<string>("Phone");

                    b.Property<double>("PricePerHour");

                    b.Property<double>("TimeToMealTicket");

                    b.HasKey("id");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("EasyTimes.Models.ServiceOrder", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AmountOfHours");

                    b.Property<bool>("CheckIn");

                    b.Property<int>("ClientID");

                    b.Property<string>("ClientName");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MealTicket");

                    b.Property<double>("MealTicketValue");

                    b.Property<double>("NormalHours");

                    b.Property<double>("OnTheRach");

                    b.Property<double>("Overtime");

                    b.Property<int>("PaymentStatus");

                    b.Property<string>("ProjectName");

                    b.Property<string>("SerialCode");

                    b.Property<DateTime>("StartDate");

                    b.Property<double>("TotalEarned");

                    b.HasKey("id");

                    b.ToTable("ServiceOrder");
                });
#pragma warning restore 612, 618
        }
    }
}

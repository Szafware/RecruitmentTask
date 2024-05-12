﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecruitmentTask.Infrastructure;

#nullable disable

namespace RecruitmentTask.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4");

            modelBuilder.Entity("RecruitmentTask.Domain.People.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "RecruitmentTask.Domain.People.Person.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int?>("ApartmentNumber")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("TEXT");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("TEXT");

                            b1.Property<string>("StreetName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("TEXT");

                            b1.Property<string>("Town")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("TEXT");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PersonalData", "RecruitmentTask.Domain.People.Person.PersonalData#PersonalData", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("BirthDateUtc")
                                .HasColumnType("TEXT");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("TEXT");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("TEXT");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(9)
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.ToTable("People", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

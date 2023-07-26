﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRUDExample.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    partial class PersonsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CountryName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("CountryID");

                    b.ToTable("countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            CountryName = "Russia"
                        },
                        new
                        {
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            CountryName = "Cameroon"
                        },
                        new
                        {
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            CountryName = "China"
                        },
                        new
                        {
                            CountryID = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            CountryName = "Nigeria"
                        },
                        new
                        {
                            CountryID = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            CountryName = "Ivory Coast"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("PersonName")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<bool>("ReceiveNewsLetters")
                        .HasColumnType("boolean");

                    b.HasKey("PersonID");

                    b.ToTable("persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonID = new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                            Address = "4 Parkside Point",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOfBirth = new DateOnly(1989, 8, 28),
                            Email = "mwebsdale0@people.com.cn",
                            Gender = "Female",
                            PersonName = "Marguerite",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                            Address = "6 Morningstar Circle",
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            DateOfBirth = new DateOnly(1990, 10, 5),
                            Email = "ushears1@globo.com",
                            Gender = "Female",
                            PersonName = "Ursa",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                            Address = "73 Heath Avenue",
                            CountryID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            DateOfBirth = new DateOnly(1995, 2, 10),
                            Email = "fbowsher2@howstuffworks.com",
                            Gender = "Male",
                            PersonName = "Franchot",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                            Address = "83187 Merry Drive",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOfBirth = new DateOnly(1987, 1, 9),
                            Email = "asarvar3@dropbox.com",
                            Gender = "Male",
                            PersonName = "Angie",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                            Address = "50467 Holy Cross Crossing",
                            CountryID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DateOfBirth = new DateOnly(1995, 2, 11),
                            Email = "ttregona4@stumbleupon.com",
                            Gender = "Gender",
                            PersonName = "Tani",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                            Address = "97570 Raven Circle",
                            CountryID = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            DateOfBirth = new DateOnly(1988, 1, 4),
                            Email = "mlingfoot5@netvibes.com",
                            Gender = "Male",
                            PersonName = "Mitchael",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("29339209-63f5-492f-8459-754943c74abf"),
                            Address = "57449 Brown Way",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOfBirth = new DateOnly(1983, 2, 16),
                            Email = "mjarrell6@wisc.edu",
                            Gender = "Male",
                            PersonName = "Maddy",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"),
                            Address = "4 Stuart Drive",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOfBirth = new DateOnly(1998, 12, 2),
                            Email = "pretchford7@virginia.edu",
                            Gender = "Female",
                            PersonName = "Pegeen",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("012107df-862f-4f16-ba94-e5c16886f005"),
                            Address = "413 Sachtjen Way",
                            CountryID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DateOfBirth = new DateOnly(1990, 9, 20),
                            Email = "hmosco8@tripod.com",
                            Gender = "Male",
                            PersonName = "Hansiain",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                            Address = "484 Clarendon Court",
                            CountryID = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            DateOfBirth = new DateOnly(1997, 9, 25),
                            Email = "lwoodwing9@wix.com",
                            Gender = "Male",
                            PersonName = "Lombard",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                            Address = "2 Warrior Avenue",
                            CountryID = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            DateOfBirth = new DateOnly(1990, 5, 24),
                            Email = "mconachya@va.gov",
                            Gender = "Female",
                            PersonName = "Minta",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                            Address = "9334 Fremont Street",
                            CountryID = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            DateOfBirth = new DateOnly(1987, 1, 19),
                            Email = "vklussb@nationalgeographic.com",
                            Gender = "Female",
                            PersonName = "Verene",
                            ReceiveNewsLetters = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

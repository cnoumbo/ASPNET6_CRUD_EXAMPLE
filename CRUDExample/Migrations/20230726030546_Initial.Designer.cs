﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRUDExample.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    [Migration("20230726030546_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("country_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("country_name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("country_id");

                    b.ToTable("countries", (string)null);

                    b.HasData(
                        new
                        {
                            country_id = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            country_name = "Russia"
                        },
                        new
                        {
                            country_id = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            country_name = "Cameroon"
                        },
                        new
                        {
                            country_id = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            country_name = "China"
                        },
                        new
                        {
                            country_id = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            country_name = "Nigeria"
                        },
                        new
                        {
                            country_id = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            country_name = "Ivory Coast"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("person_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("address")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid?>("country_id")
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("date_of_birth")
                        .HasColumnType("date");

                    b.Property<string>("email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("gender")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("person_name")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<bool>("receive_newsletters")
                        .HasColumnType("boolean");

                    b.HasKey("person_id");

                    b.ToTable("persons", (string)null);

                    b.HasData(
                        new
                        {
                            person_id = new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                            address = "4 Parkside Point",
                            country_id = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            date_of_birth = new DateOnly(1989, 8, 28),
                            email = "mwebsdale0@people.com.cn",
                            gender = "Female",
                            person_name = "Marguerite",
                            receive_newsletters = false
                        },
                        new
                        {
                            person_id = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                            address = "6 Morningstar Circle",
                            country_id = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            date_of_birth = new DateOnly(1990, 10, 5),
                            email = "ushears1@globo.com",
                            gender = "Female",
                            person_name = "Ursa",
                            receive_newsletters = false
                        },
                        new
                        {
                            person_id = new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                            address = "73 Heath Avenue",
                            country_id = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            date_of_birth = new DateOnly(1995, 2, 10),
                            email = "fbowsher2@howstuffworks.com",
                            gender = "Male",
                            person_name = "Franchot",
                            receive_newsletters = true
                        },
                        new
                        {
                            person_id = new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                            address = "83187 Merry Drive",
                            country_id = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            date_of_birth = new DateOnly(1987, 1, 9),
                            email = "asarvar3@dropbox.com",
                            gender = "Male",
                            person_name = "Angie",
                            receive_newsletters = true
                        },
                        new
                        {
                            person_id = new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                            address = "50467 Holy Cross Crossing",
                            country_id = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            date_of_birth = new DateOnly(1995, 2, 11),
                            email = "ttregona4@stumbleupon.com",
                            gender = "gender",
                            person_name = "Tani",
                            receive_newsletters = false
                        },
                        new
                        {
                            person_id = new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                            address = "97570 Raven Circle",
                            country_id = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            date_of_birth = new DateOnly(1988, 1, 4),
                            email = "mlingfoot5@netvibes.com",
                            gender = "Male",
                            person_name = "Mitchael",
                            receive_newsletters = false
                        },
                        new
                        {
                            person_id = new Guid("29339209-63f5-492f-8459-754943c74abf"),
                            address = "57449 Brown Way",
                            country_id = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            date_of_birth = new DateOnly(1983, 2, 16),
                            email = "mjarrell6@wisc.edu",
                            gender = "Male",
                            person_name = "Maddy",
                            receive_newsletters = true
                        },
                        new
                        {
                            person_id = new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"),
                            address = "4 Stuart Drive",
                            country_id = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            date_of_birth = new DateOnly(1998, 12, 2),
                            email = "pretchford7@virginia.edu",
                            gender = "Female",
                            person_name = "Pegeen",
                            receive_newsletters = true
                        },
                        new
                        {
                            person_id = new Guid("012107df-862f-4f16-ba94-e5c16886f005"),
                            address = "413 Sachtjen Way",
                            country_id = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            date_of_birth = new DateOnly(1990, 9, 20),
                            email = "hmosco8@tripod.com",
                            gender = "Male",
                            person_name = "Hansiain",
                            receive_newsletters = true
                        },
                        new
                        {
                            person_id = new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                            address = "484 Clarendon Court",
                            country_id = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            date_of_birth = new DateOnly(1997, 9, 25),
                            email = "lwoodwing9@wix.com",
                            gender = "Male",
                            person_name = "Lombard",
                            receive_newsletters = false
                        },
                        new
                        {
                            person_id = new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                            address = "2 Warrior Avenue",
                            country_id = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            date_of_birth = new DateOnly(1990, 5, 24),
                            email = "mconachya@va.gov",
                            gender = "Female",
                            person_name = "Minta",
                            receive_newsletters = true
                        },
                        new
                        {
                            person_id = new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                            address = "9334 Fremont Street",
                            country_id = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            date_of_birth = new DateOnly(1987, 1, 19),
                            email = "vklussb@nationalgeographic.com",
                            gender = "Female",
                            person_name = "Verene",
                            receive_newsletters = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VolcanoFinder.API.DbContexts;

#nullable disable

namespace VolcanoFinder.API.Migrations
{
    [DbContext(typeof(VolcanoFinderContext))]
    [Migration("20220622154429_SeedDatabase")]
    partial class SeedDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("VolcanoFinder.API.Models.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Africa"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Asia"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Australia"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Europe"
                        },
                        new
                        {
                            Id = 5,
                            Name = "North America"
                        },
                        new
                        {
                            Id = 6,
                            Name = "South America"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Antarctica"
                        });
                });

            modelBuilder.Entity("VolcanoFinder.API.Models.Entities.Volcano", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CountryAlpha2")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastEruption")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RegionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Volcanoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            CountryAlpha2 = "IT",
                            Description = "Mount Etna is an active stratovolcano on the east coast of Sicily, Italy, in the Metropolitan City of Catania, between the cities of Messina and Catania.",
                            LastEruption = new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mount Etna",
                            Picture = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Mt_Etna_and_Catania.JPG",
                            RegionId = 4
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            CountryAlpha2 = "JP",
                            Description = "Mount Fuji is located about 100 km (62 mi) southwest of Tokyo and is visible from there on clear days. Mount Fuji's exceptionally symmetrical cone, which is covered in snow for about five months of the year, is commonly used as a cultural icon of Japan and it is frequently depicted in art and photography, as well as visited by sightseers and climbers.",
                            LastEruption = new DateTime(1708, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mount Fuji",
                            Picture = "https://upload.wikimedia.org/wikipedia/commons/1/1b/080103_hakkai_fuji.jpg",
                            RegionId = 2
                        },
                        new
                        {
                            Id = 3,
                            Active = false,
                            CountryAlpha2 = "TZ",
                            Description = "Mount Kilimanjaro is a dormant volcano in United Republic of Tanzania. It has three volcanic cones: Kibo, Mawenzi, and Shira. It is the highest mountain in Africa and the highest single free-standing mountain above sea level in the world: 5,895 metres (19,341 ft) above sea level and about 4,900 metres (16,100 ft) above its plateau base.",
                            Name = "Mount Kilimanjaro",
                            Picture = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Mt._Kilimanjaro_12.2006.JPG",
                            RegionId = 1
                        });
                });

            modelBuilder.Entity("VolcanoFinder.API.Models.Entities.Volcano", b =>
                {
                    b.HasOne("VolcanoFinder.API.Models.Entities.Region", "Region")
                        .WithMany("Volcanoes")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("VolcanoFinder.API.Models.Entities.Region", b =>
                {
                    b.Navigation("Volcanoes");
                });
#pragma warning restore 612, 618
        }
    }
}

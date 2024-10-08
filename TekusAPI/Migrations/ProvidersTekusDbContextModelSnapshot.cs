﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TekusAPI.Data;

#nullable disable

namespace TekusAPI.Migrations
{
    [DbContext(typeof(ProvidersTekusDbContext))]
    partial class ProvidersTekusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceCountry", b =>
                {
                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("CountryId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceCountry");
                });

            modelBuilder.Entity("TekusAPI.Models.Country", b =>
                {
                    b.Property<int>("IdCountry")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCountry"));

                    b.Property<string>("CommonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("IdCountry");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("TekusAPI.Models.CustomField", b =>
                {
                    b.Property<int>("IdCustomField")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomField"));

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int?>("ServicesProviderIdServices")
                        .HasColumnType("int");

                    b.HasKey("IdCustomField");

                    b.HasIndex("ProviderId");

                    b.HasIndex("ServicesProviderIdServices");

                    b.ToTable("CustomFields");
                });

            modelBuilder.Entity("TekusAPI.Models.ProvidersTekus", b =>
                {
                    b.Property<int>("IdProviders")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProviders"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NIT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProviders");

                    b.ToTable("ProvidersTekus");
                });

            modelBuilder.Entity("TekusAPI.Models.ServicesProvider", b =>
                {
                    b.Property<int>("IdServices")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServices"));

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("IdServices");

                    b.HasIndex("ProviderId");

                    b.ToTable("ServicesProvider");
                });

            modelBuilder.Entity("ServiceCountry", b =>
                {
                    b.HasOne("TekusAPI.Models.Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TekusAPI.Models.ServicesProvider", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TekusAPI.Models.CustomField", b =>
                {
                    b.HasOne("TekusAPI.Models.ProvidersTekus", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TekusAPI.Models.ServicesProvider", null)
                        .WithMany("CustomFields")
                        .HasForeignKey("ServicesProviderIdServices");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("TekusAPI.Models.ServicesProvider", b =>
                {
                    b.HasOne("TekusAPI.Models.ProvidersTekus", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("TekusAPI.Models.ServicesProvider", b =>
                {
                    b.Navigation("CustomFields");
                });
#pragma warning restore 612, 618
        }
    }
}

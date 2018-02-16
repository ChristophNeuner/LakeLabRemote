﻿// <auto-generated />
using LakeLabLib;
using LakeLabRemote.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LakeLabRemote.Migrations.LakeLabDb
{
    [DbContext(typeof(LakeLabDbContext))]
    [Migration("20180216135657_LakeLabDb2")]
    partial class LakeLabDb2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("LakeLabRemote.Models.AppUserDevice", b =>
                {
                    b.Property<string>("ComposedKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppUserId");

                    b.Property<Guid>("DeviceId");

                    b.HasKey("ComposedKey");

                    b.ToTable("AppUserDeviceAssociation");
                });

            modelBuilder.Entity("LakeLabRemote.Models.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Depth");

                    b.Property<string>("Ip");

                    b.Property<string>("Lake");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<DateTime>("TimeOfCreation");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("LakeLabRemote.Models.Value", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Data");

                    b.Property<string>("DataUnit");

                    b.Property<Guid?>("DeviceId");

                    b.Property<int>("SensorType");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Guid");

                    b.HasIndex("DeviceId");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("LakeLabRemote.Models.Value", b =>
                {
                    b.HasOne("LakeLabRemote.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");
                });
#pragma warning restore 612, 618
        }
    }
}

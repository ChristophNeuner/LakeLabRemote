﻿// <auto-generated />
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
    partial class LakeLabDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("LakeLabRemote.Models.Device", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Depth");

                    b.Property<string>("Ip");

                    b.Property<string>("Lake");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<DateTime>("TimeOfCreation");

                    b.HasKey("Guid");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("LakeLabRemote.Models.ValueDO", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Data");

                    b.Property<Guid?>("DeviceGuid");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Guid");

                    b.HasIndex("DeviceGuid");

                    b.ToTable("ValuesDO");
                });

            modelBuilder.Entity("LakeLabRemote.Models.ValueDO", b =>
                {
                    b.HasOne("LakeLabRemote.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceGuid");
                });
#pragma warning restore 612, 618
        }
    }
}

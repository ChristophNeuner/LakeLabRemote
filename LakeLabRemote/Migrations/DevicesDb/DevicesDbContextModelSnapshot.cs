﻿// <auto-generated />
using LakeLabRemote.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LakeLabRemote.Migrations.DevicesDb
{
    [DbContext(typeof(DevicesDbContext))]
    partial class DevicesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("LakeLabRemote.Models.Device", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Depth");

                    b.Property<string>("Ip");

                    b.Property<string>("Location");

                    b.HasKey("Name");

                    b.ToTable("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}

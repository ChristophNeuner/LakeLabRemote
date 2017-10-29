using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models;

namespace LakeLabRemote.DataSource
{
    public class LakeLabDbContext : DbContext
    {
        public LakeLabDbContext(DbContextOptions<LakeLabDbContext> options) : base(options){}

        public DbSet<Device> Devices { get; set; }
        public DbSet<ValueDO> ValuesDO { get; set; } 
        public DbSet<AppUserDevice> AppUserDeviceAssociation { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppUserDevice>()
        //.HasKey(aud => new { aud.AppUserId, aud.DeviceId });

        //    modelBuilder.Entity<AppUserDevice>()
        //        .HasOne(aud => aud.User)
        //        .WithMany(au => au.AppUserDevices)
        //        .HasForeignKey(aud => aud.AppUserId);

        //    modelBuilder.Entity<AppUserDevice>()
        //        .HasOne(aud => aud.Device)
        //        .WithMany(d => d.AppUserDevices)
        //        .HasForeignKey(aud => aud.DeviceId);
        //}
    }
}

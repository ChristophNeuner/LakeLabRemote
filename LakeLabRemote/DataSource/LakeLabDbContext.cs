using Microsoft.EntityFrameworkCore;
using LakeLabRemote.Models;
using System.Collections.Generic;

namespace LakeLabRemote.DataSource
{
    public class LakeLabDbContext : DbContext
    {
        public LakeLabDbContext(DbContextOptions<LakeLabDbContext> options) : base(options){}

        public DbSet<Device> Devices { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<AppUserDevice> AppUserDeviceAssociation { get; set; }    
        public DbSet<Sensor> Sensors { get; set; }
    }
}

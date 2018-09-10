using Microsoft.EntityFrameworkCore;
using LakeLabRemote.Models;

namespace LakeLabRemote.DataSource
{
    public class LakeLabDbContext : DbContext
    {
        public LakeLabDbContext(DbContextOptions<LakeLabDbContext> options) : base(options){}

        public DbSet<Device> Devices { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<AppUserDevice> AppUserDeviceAssociation { get; set; }       
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LakeLabRemote.DataSource
{
    public class LakeLabDbContext : DbContext
    {
        public LakeLabDbContext(DbContextOptions<LakeLabDbContext> options) : base(options){}

        public DbSet<Device> Devices { get; set; }
        public DbSet<ValueDO> ValuesDO { get; set; }
        public DbSet<ValueTemp> ValuesTemp { get; set; }
        public DbSet<AppUserDevice> AppUserDeviceAssociation { get; set; }       
    }
}

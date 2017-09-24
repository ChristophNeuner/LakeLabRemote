using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models;

namespace LakeLabRemote.DataSource
{
    public class ValuesDbContext : DbContext
    {
        public ValuesDbContext(DbContextOptions<ValuesDbContext> options) : base(options){ }

        public DbSet<ValueDO> ValuesDO{ get; set; }
    }
}

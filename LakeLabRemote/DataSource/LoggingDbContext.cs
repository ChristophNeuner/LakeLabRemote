using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.DataSource
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options) { }

        public DbSet<UserRequest> UserRequests { get; set; }
    }
}

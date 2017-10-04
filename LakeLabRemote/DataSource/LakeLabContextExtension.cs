using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.DataSource
{
    public static class LakeLabContextExtension
    {
        public static async Task<IEnumerable<Device>> QueryDevicesAsync(this LakeLabDbContext context, string name)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return await context.Devices.Where(p => p.Name == name).ToListAsync();
        }

        public static async Task<IEnumerable<T>> QueryValuesAsync<T>(this LakeLabDbContext context, Func<IQueryable<Value>, IQueryable<T>> queryShaper)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (queryShaper == null)
            {
                throw new ArgumentNullException(nameof(queryShaper));
            }

            return await queryShaper(context.ValuesDO.Include(p => p.Device)).ToListAsync();
        }

        public static Task<T> QueryValuesAsync<T>(this LakeLabDbContext context, Func<IQueryable<Value>, Task<T>> queryShaper)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (queryShaper == null)
            {
                throw new ArgumentNullException(nameof(queryShaper));
            }

            return queryShaper(context.ValuesDO.Include(p => p.Device));
        }
    }
}

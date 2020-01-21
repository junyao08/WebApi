using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Test.Models;

namespace WebApi.Test.Contexts
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Items> Items { get; set; }
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {

        }
    }
}

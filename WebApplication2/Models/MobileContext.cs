using System;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Sample.Web.Models
{
    public class MobileContext : DbContext
    {
        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}

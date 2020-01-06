using Microsoft.EntityFrameworkCore;
using MultiVendorAPI.Dtos;
using MultiVendorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiVendorAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Users> users { get; set; }

    }
}

using FederataFutbollit.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FederataFutbollit.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Superliga> Superligat { get; set; }
        public DbSet<Selektori>Selektort{get;set;}
        public DbSet<Stafi> Stafi { get; set; }
    }


}

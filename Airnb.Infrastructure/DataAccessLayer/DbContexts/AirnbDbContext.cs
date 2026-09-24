using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.DataAccessLayer.DbContexts
{
    public class AirnbDbContext : DbContext
    {
        public AirnbDbContext(DbContextOptions<AirnbDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<GuestProfile> Guests => Set<GuestProfile>();
        public DbSet<Home> Homes => Set<Home>();
        public DbSet<HomeRating> HomeRatings => Set<HomeRating>();
        public DbSet<HostProfile> Hosts => Set<HostProfile>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<User> Users => Set<User>();
       
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AirnbDbContext).Assembly);
        }
    }
}

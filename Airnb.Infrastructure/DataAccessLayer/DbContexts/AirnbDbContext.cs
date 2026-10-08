using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.DataAccessLayer.DbContexts
{
    public class AirnbDbContext : DbContext //det vigtigeste denne klasse skal gøre er at tildele nogle tabels til de forskellige entiteter,
                                            //som vi har i vores domæne. Det gør vi ved at definere DbSet properties for hver entitet.
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
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();




        protected override void OnModelCreating(ModelBuilder modelBuilder) //til konventioner og regler for hvordan entiteterne skal mappes til databasen.
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AirnbDbContext).Assembly); //Dette vil automatisk registrere alle konfigurationer,
                                                                                           //der er defineret i separate konfigurationsklasser,
                                                                                           //som implementerer IEntityTypeConfiguration<T> interfacet.
                                                                                           //- kig i mappen Configurations for at se hvordan det er gjort.
        }
    }
}

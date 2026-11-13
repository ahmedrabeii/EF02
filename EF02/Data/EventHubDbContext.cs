using EF02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EF02.Data
{
    public class EventHubDbContext : DbContext
    {
        public DbSet<Organizer> Organizers { get; set; }

        public DbSet<OrganizerProfile> OrganizerProfiles { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Badge> Badges { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // OrganizerProfile - One To One
            modelBuilder.Entity<OrganizerProfile>()
                .HasKey(p => p.OrganizerProfileId);

            modelBuilder.Entity<OrganizerProfile>()
                .HasOne(p => p.Organizer)
                .WithOne(o => o.Profile)
                .HasForeignKey<OrganizerProfile>(
                    p => p.OrganizerId)
                .IsRequired();

            // Badge - One To One
            modelBuilder.Entity<Badge>()
                .HasKey(b => b.BadgeId);

            modelBuilder.Entity<Badge>()
                .HasIndex(b => b.BadgeNumber)
                .IsUnique();

            modelBuilder.Entity<Badge>()
                .Property(b => b.Tier)
                .HasConversion<string>();

            modelBuilder.Entity<Badge>()
                .HasOne(b => b.Attendee)
                .WithOne(a => a.Badge)
                .HasForeignKey<Badge>(
                    b => b.AttendeeId)
                .IsRequired();

            // Event Configuration
            modelBuilder.ApplyConfiguration(
                new EventConfiguration());

        }
    }
}

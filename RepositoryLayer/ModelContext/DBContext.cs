using Microsoft.EntityFrameworkCore;
using System;
using CommonLayer.Model;

namespace RepositoryLayer.ModelContext
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Constituency> constituencies { get; set; }
        public DbSet<AdminConstituency> adminConstituencies { get; set; }
        public DbSet<Party> parties { get; set; }
        public DbSet<AdminParty> adminParties { get; set; }
        public DbSet<Candidate> candidates { get; set; }
        public DbSet<AdminCandidate> adminCandidates { get; set; }
        public DbSet<Voter> voters { get; set; }
        public DbSet<AdminVoter> adminVoters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Candidate>()
                .HasIndex(phone => phone.CandidatePhoneNumber)
                .IsUnique();

                modelBuilder.Entity<Admin>()
               .HasIndex(email => email.AdmEmail)
               .IsUnique();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

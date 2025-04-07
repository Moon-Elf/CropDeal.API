using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropListing> CropListings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankAccount>()
                .HasOne(b => b.User)
                .WithMany(u => u.BankAccounts)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Crop)
                .WithMany(c => c.Subscriptions)
                .HasForeignKey(s => s.CropId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Dealer)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.DealerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CropListing>()
                .HasOne(cl => cl.Crop)
                .WithMany(c => c.CropListings)
                .HasForeignKey(cl => cl.CropId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CropListing>()
                .HasOne(cl => cl.Farmer)
                .WithMany(u => u.CropListings)
                .HasForeignKey(cl => cl.FarmerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.GeneratedBy)
                .WithMany(u => u.GeneratedReports)
                .HasForeignKey(r => r.GeneratedById)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Report>()
                .HasOne(r => r.GeneratedFor)
                .WithMany(u => u.ReceivedReports)
                .HasForeignKey(r => r.GeneratedForId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Dealer)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.DealerId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.PurchasingCrop)
                .WithMany()
                .HasForeignKey(t => t.ListingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Dealer)
                .WithMany(u => u.GivenReviews)
                .HasForeignKey(r => r.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Farmer)
                .WithMany(u => u.ReceivedReviews)
                .HasForeignKey(r => r.FarmerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Transaction)
                .WithOne()
                .HasForeignKey<Review>(r => r.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
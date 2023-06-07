using Core.Entities;
using Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
            
        }
       public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ImagesEntity> Images { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<AppUser> Registers { get; set; }
        public DbSet<DonationEntity> Donations { get; set; }
        public DbSet<ProjectReportEntity> ProjectReports { get; set; }
        public DbSet<CommentReportEntity> CommentReports { get; set; }
       public DbSet<ReplyEntity> Replies { get; set; }
        public DbSet<RateEntity> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<CommentEntity>()
       .HasOne(c => c.User)
       .WithMany()
       .HasForeignKey(c => c.UserId)
       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DonationEntity>()
       .HasOne(c => c.User)
       .WithMany()
       .HasForeignKey(c => c.UserId)
       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectReportEntity>()
      .HasOne(c => c.User)
      .WithMany()
      .HasForeignKey(c => c.UserId)
      .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RateEntity>()
     .HasOne(c => c.User)
     .WithMany()
     .HasForeignKey(c => c.UserId)
     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentReportEntity>()
     .HasOne(c => c.User)
     .WithMany()
     .HasForeignKey(c => c.UserId)
     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReplyEntity>()
  .HasOne(c => c.User)
  .WithMany()
  .HasForeignKey(c => c.UserId)
  .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);


        }
    }
}

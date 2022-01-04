using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CustomerFeedbackDA.Model;

#nullable disable

namespace CustomerFeedbackDA.Data
{
    public partial class CustomerFeedbackContext : DbContext
    {
        public CustomerFeedbackContext()
        {
        }

        public CustomerFeedbackContext(DbContextOptions<CustomerFeedbackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FeedbackNumber).IsUnicode(false);

                entity.Property(e => e.LastPurchasedItem).IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__Produc__5812160E");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__Review__59063A47");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__UserId__59FA5E80");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__4316F928");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Initial).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBFirstPlannerAPI.Models
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SessionAttendees> SessionAttendees { get; set; }
        public virtual DbSet<SessionTypes> SessionTypes { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<Users> Users { get; set; }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TestDB;User ID=sa;Password=<YourStrong@Passw0rd>;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionAttendees>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SessionId })
                    .HasName("PK__Session___6F2524F2FE3C588E");

                entity.ToTable("Session_Attendees");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionAttendees)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session_A__sessi__32E0915F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionAttendees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session_A__user___31EC6D26");
            });

            modelBuilder.Entity<SessionTypes>(entity =>
            {
                entity.ToTable("Session_Types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SessionType)
                    .IsRequired()
                    .HasColumnName("session_type")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateHeld)
                    .HasColumnName("date_held")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Organizer).HasColumnName("organizer");

                entity.Property(e => e.SessionTypeId).HasColumnName("session_type_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrganizerNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Organizer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sessions__organi__2E1BDC42");

                entity.HasOne(d => d.SessionType)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.SessionTypeId)
                    .HasConstraintName("FK__Sessions__sessio__2F10007B");
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User_Det__B9BE370F705D696A");

                entity.ToTable("User_Details");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserDetails)
                    .HasForeignKey<UserDetails>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Deta__user___29572725");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("unique_email")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("unique_username")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSat)
                    .IsRequired()
                    .HasColumnName("password_sat")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

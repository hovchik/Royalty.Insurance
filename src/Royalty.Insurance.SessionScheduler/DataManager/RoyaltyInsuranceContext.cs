using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.SessionScheduler.DataManager.Domain;

namespace Royalty.Insurance.SessionScheduler.DataManager
{
    public class RoyaltyInsuranceContext : DbContext
    {
        public RoyaltyInsuranceContext(DbContextOptions<RoyaltyInsuranceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersProfile> UsersProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AdditionalPhone).IsUnicode(false);

                entity.Property(e => e.CellPhone).IsUnicode(false);

                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ForgetPasswordCode).IsUnicode(false);

                entity.Property(e => e.HomePhone).IsUnicode(false);

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password).IsFixedLength(true);

                entity.Property(e => e.Salting).IsFixedLength(true);

                entity.Property(e => e.TemporaryPassword).HasDefaultValueSql("((1))");

                entity.Property(e => e.WorkPhone).IsUnicode(false);

            });


            modelBuilder.Entity<UsersProfile>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserLastStatusId).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.UserId)
                    .WithOne(p => p.UsersProfile)
                    .HasForeignKey<UsersProfile>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersProfiles_Users");

            });

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.SessionId).IsRequired();

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.UserId).IsRequired();

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.DeviceIp).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.RefreshToken).HasMaxLength(64).IsRequired();

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.RefreshTokenExpireAt).IsRequired();

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.LogInDatetimeUtc).IsRequired();

            modelBuilder.Entity<UserActivityLog>()
                .Property(p => p.LogOutDatetimeUtc);
        }
    }
}

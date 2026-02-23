using MicroNotify.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroNotify.Notification.Infrastructure.Persistence
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Notification> Notifications => Set<Domain.Entities.Notification>();
        public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Notification configuration
            modelBuilder.Entity<Domain.Entities.Notification>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.UserId).IsRequired();
                entity.Property(n => n.Title).IsRequired().HasMaxLength(200);
                entity.Property(n => n.Message).IsRequired().HasMaxLength(1000);
                entity.Property(n => n.Type).IsRequired();
                entity.Property(n => n.IsRead).IsRequired();
                entity.Property(n => n.CreatedAt).IsRequired();
                entity.HasIndex(n => new { n.UserId, n.CreatedAt });
            });

            // NotificationTemplate configuration
            modelBuilder.Entity<NotificationTemplate>(entity =>
            {
                entity.HasKey(nt => nt.Id);
                entity.Property(nt => nt.EventType).IsRequired().HasMaxLength(100);
                entity.Property(nt => nt.Title).IsRequired().HasMaxLength(200);
                entity.Property(nt => nt.MessageTemplate).IsRequired().HasMaxLength(1000);
                entity.HasIndex(nt => nt.EventType).IsUnique();
            });
        }
    }
}

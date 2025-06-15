using Fiap.Api.EnvironmentalAlert.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.EnvironmentalAlert.Data.Contexts
{
    public class DatabaseContext : DbContext // isso a classe que representa o banco de dados e é usada para interagir com ele, ela que faz a conexão com o banco de dados e define as entidades que serão mapeadas para as tabelas do banco de dados.
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DeviceModel> Devices { get; set; } = null!;
        public DbSet<DeviceConsumptionModel> DeviceConsumptions { get; set; } = null!;
        public DbSet<ConsumptionAlertModel> ConsumptionAlerts { get; set; } = null!;

        protected DatabaseContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //isso que vai permitir que o Entity Framework Core crie as tabelas no banco de dados com base nas entidades definidas na aplicação, e também permite que você configure relacionamentos entre as entidades, como chaves estrangeiras e índices.
        {
            modelBuilder.HasDefaultSchema("RM558332");
            modelBuilder.Entity<DeviceModel>(entity =>
            {
                entity.ToTable("DEVICE");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .HasColumnName("NAME")
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Location)
                      .HasColumnName("LOCATION")
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<DeviceConsumptionModel>(entity =>
            {
                entity.ToTable("DEVICE_CONSUMPTION");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.DeviceId)
                      .HasColumnName("DEVICE_ID");

                entity.Property(e => e.ConsumptionKwh)
                      .HasColumnName("CONSUMPTION_KWH")
                      .HasColumnType("NUMBER(10,2)");

                entity.Property(e => e.ExpectedLimitKwh)
                      .HasColumnName("EXPECTED_LIMIT_KWH")
                      .HasColumnType("NUMBER(10,2)");

                entity.Property(e => e.RecordedAt)
                      .HasColumnName("RECORDED_AT")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Device)
                      .WithMany(d => d.DeviceConsumptions)
                      .HasForeignKey(e => e.DeviceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ConsumptionAlertModel>(entity =>
            {
                entity.ToTable("CONSUMPTION_ALERT");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("ID")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.DeviceId)
                      .HasColumnName("DEVICE_ID");

                entity.Property(e => e.RecordedConsumption)
                      .HasColumnName("RECORDED_CONSUMPTION")
                      .HasColumnType("NUMBER(10,2)");

                entity.Property(e => e.ExpectedLimit)
                      .HasColumnName("EXPECTED_LIMIT")
                      .HasColumnType("NUMBER(10,2)");

                entity.Property(e => e.Message)
                      .HasColumnName("MESSAGE")
                      .HasMaxLength(255);

                entity.Property(e => e.AlertAt)
                      .HasColumnName("ALERT_AT")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Device)
                      .WithMany(d => d.ConsumptionAlerts)
                      .HasForeignKey(e => e.DeviceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

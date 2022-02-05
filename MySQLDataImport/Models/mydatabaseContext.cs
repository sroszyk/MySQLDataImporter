using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MySQLDataImport.Models
{
    public partial class mydatabaseContext : DbContext
    {
        private readonly string connectionString;
        public mydatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public mydatabaseContext(DbContextOptions<mydatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gwtable> Gwtables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gwtable>(entity =>
            {
                entity.HasKey(e => e.Idgwtable)
                    .HasName("PRIMARY");

                entity.ToTable("gwtable");

                entity.Property(e => e.Idgwtable).HasColumnName("idgwtable");

                entity.Property(e => e.DodatkoweObciazenieNalezn).HasColumnName("DodatkoweObciazenieNalezn.");

                entity.Property(e => e.NazwaWsiLubUlicyNumerDomu).HasMaxLength(150);

                entity.Property(e => e.NazwiskoImieDoOplatyNaleznosci).HasMaxLength(150);

                entity.Property(e => e.NrKolejny).HasColumnName("Nr.kolejny");

                entity.Property(e => e.PowMeliorowana).HasColumnName("Pow.meliorowana");

                entity.Property(e => e.PozRej).HasColumnName("Poz.rej");

                entity.Property(e => e.Saldo).HasColumnName("SALDO");

                entity.Property(e => e.UdzialUlamkowyWpozRej)
                    .HasMaxLength(10)
                    .HasColumnName("UdzialUlamkowyWPoz.rej");

                entity.Property(e => e.WysokoscWplatyWtZalegl).HasColumnName("WysokoscWplatyWT.Zalegl");

                entity.Property(e => e.ZalegloscZlatUbieglych).HasColumnName("ZalegloscZLatUbieglych");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

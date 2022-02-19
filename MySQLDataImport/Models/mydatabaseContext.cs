using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MySQLDataImport.Models
{
    public partial class mydatabaseContext : DbContext
    {
        private readonly string connectionString;

        public mydatabaseContext(string conntectionstring)
        {
            connectionString = conntectionstring;
        }

        public mydatabaseContext(DbContextOptions<mydatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dzialki> Dzialkis { get; set; }
        public virtual DbSet<Gwtable> Gwtables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dzialki>(entity =>
            {
                entity.HasKey(e => e.Iddzialki)
                    .HasName("PRIMARY");

                entity.ToTable("dzialki");

                entity.HasIndex(e => e.PozRej, "PozRej");

                entity.Property(e => e.Iddzialki).HasColumnName("iddzialki");

                entity.Property(e => e.NrDzialki).HasMaxLength(45);

                entity.HasOne(e => e.Gwtable).WithMany(e => e.Dzialki).HasForeignKey(e => e.PozRej);
            });


            modelBuilder.Entity<Gwtable>(entity =>
            {
                entity.HasKey(e => e.Idgwtable)
                    .HasName("PRIMARY");

                entity.ToTable("gwtable");

                entity.HasIndex(e => e.NrKolejny, "part_of_name");

                entity.HasIndex(e => e.PozRej, "poz_rej_idx");

                entity.Property(e => e.Idgwtable).HasColumnName("idgwtable");

                entity.Property(e => e.DodatkoweObciazenieNalezn).HasColumnName("DodatkoweObciazenieNalezn.");

                entity.Property(e => e.KodPocztowy).HasMaxLength(150);

                entity.Property(e => e.Miasto).HasMaxLength(150);

                entity.Property(e => e.NazwaWsiLubUlicyNumerDomu).HasMaxLength(150);

                entity.Property(e => e.NazwiskoImieDoOplatyNaleznosci).HasMaxLength(150);

                entity.Property(e => e.NrKolejny).HasColumnName("Nr.kolejny");

                entity.Property(e => e.PowMeliorowana).HasColumnName("Pow.meliorowana");

                entity.Property(e => e.PozRej).HasColumnName("Poz.rej");

                entity.Property(e => e.Saldo).HasColumnName("SALDO");

                entity.Property(e => e.UdzialUlamkowyWpozRej)
                    .HasMaxLength(10)
                    .HasColumnName("UdzialUlamkowyWPoz.rej");

                entity.Property(e => e.Ulica).HasMaxLength(150);

                entity.Property(e => e.WysokoscWplatyWtZalegl).HasColumnName("WysokoscWplatyWT.Zalegl");

                entity.Property(e => e.ZalegloscZlatUbieglych).HasColumnName("ZalegloscZLatUbieglych");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

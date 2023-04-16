using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace clinicacaritafelizback.Models
{
    public partial class CLINICAContext : DbContext
    {
        public CLINICAContext()
        {
        }

        public CLINICAContext(DbContextOptions<CLINICAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aseguradora> Aseguradora { get; set; }
        public virtual DbSet<Cita> Cita { get; set; }
        public virtual DbSet<Comprobante> Comprobante { get; set; }
        public virtual DbSet<DetalleComprobante> DetalleComprobante { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tarifa> Tarifa { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ROSAURA;Initial Catalog=CLINICA;Integrated Security=SSPI; User ID=clinica;Password=123456; integrated security=True;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Aseguradora>(entity =>
            {
                entity.HasKey(e => e.IdAseguradora)
                    .HasName("Aseguradora_pk");

                entity.Property(e => e.IdAseguradora)
                    .HasColumnName("idAseguradora")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("cita_pk");

                entity.Property(e => e.IdCita)
                    .HasColumnName("idCita")
                    .ValueGeneratedNever();

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fechaHora")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAseguradora).HasColumnName("idAseguradora");

                entity.Property(e => e.IdMedico).HasColumnName("idMedico");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.NumeroPoliza).HasColumnName("numeroPoliza");

                entity.HasOne(d => d.IdAseguradoraNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdAseguradora)
                    .HasConstraintName("FK_Cita_idAseguradora");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdMedico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_idMedico");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_idPaciente");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_idUsuario");
            });

            modelBuilder.Entity<Comprobante>(entity =>
            {
                entity.HasKey(e => e.IdComprobante)
                    .HasName("PK__Comproba__BF4D8CF391BDB755");

                entity.Property(e => e.IdComprobante)
                    .HasColumnName("idComprobante")
                    .ValueGeneratedNever();

                entity.Property(e => e.FechaEmision)
                    .HasColumnName("fechaEmision")
                    .HasColumnType("date");

                entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnName("numero")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasColumnName("serie")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("valorTotal")
                    .HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.Comprobante)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comprobante_idCita");

                entity.HasOne(d => d.IdTipoDocumentoNavigation)
                    .WithMany(p => p.Comprobante)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comprobante_idTipoDocumento");
            });

            modelBuilder.Entity<DetalleComprobante>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("DetalleComprobante_pk");

                entity.Property(e => e.IdDetalle)
                    .HasColumnName("idDetalle")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdComprobante).HasColumnName("idComprobante");

                entity.Property(e => e.IdTarifa).HasColumnName("idTarifa");

                entity.Property(e => e.Igv)
                    .HasColumnName("igv")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnName("precioUnitario")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.SubTotal)
                    .HasColumnName("subTotal")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("valorTotal")
                    .HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.IdComprobanteNavigation)
                    .WithMany(p => p.DetalleComprobante)
                    .HasForeignKey(d => d.IdComprobante)
                    .HasConstraintName("FK_DetalleComprobante_idComprobante");

                entity.HasOne(d => d.IdTarifaNavigation)
                    .WithMany(p => p.DetalleComprobante)
                    .HasForeignKey(d => d.IdTarifa)
                    .HasConstraintName("FK_DetalleComprobante_idTarifa");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad)
                    .HasName("Especialidad_pk");

                entity.Property(e => e.IdEspecialidad)
                    .HasColumnName("idEspecialidad")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.IdMedico)
                    .HasName("Medico_pk");

                entity.Property(e => e.IdMedico)
                    .HasColumnName("idMedico")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Celular).HasColumnName("celular");

                entity.Property(e => e.Cmp)
                    .IsRequired()
                    .HasColumnName("cmp")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dni).HasColumnName("dni");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("sexo")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.Medico)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .HasConstraintName("FK_Medico_idEspecialidad");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("Paciente_pk");

                entity.Property(e => e.IdPaciente)
                    .HasColumnName("idPaciente")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Celular).HasColumnName("celular");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dni).HasColumnName("dni");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("sexo")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("Rol_pk");

                entity.Property(e => e.IdRol)
                    .HasColumnName("idRol")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tarifa>(entity =>
            {
                entity.HasKey(e => e.IdTarifa)
                    .HasName("Tarifa_pk");

                entity.Property(e => e.IdTarifa)
                    .HasColumnName("idTarifa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioAsegurado)
                    .HasColumnName("precioAsegurado")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.PrecioParticular)
                    .HasColumnName("precioParticular")
                    .HasColumnType("decimal(4, 2)");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumento)
                    .HasName("TipoDocumento_pk");

                entity.Property(e => e.IdTipoDocumento)
                    .HasColumnName("idTipoDocumento")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("Usuario_pk");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("idUsuario")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Celular).HasColumnName("celular");

                entity.Property(e => e.Dni).HasColumnName("dni");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("sexo")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Usuario_idRol");
            });
        }
    }
}

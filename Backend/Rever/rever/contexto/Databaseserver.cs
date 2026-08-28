using Microsoft.EntityFrameworkCore;
using rever.Models;

namespace rever.contexto
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
        }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Barrio> Barrio { get; set; }
        public DbSet<TipoInmueble> TipoInmueble { get; set; }
        public DbSet<EstadoPublicacion> EstadoPublicacion { get; set; }
        public DbSet<Inmueble> Inmueble { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Caracteristica> Caracteristica { get; set; }
        public DbSet<InmuebleCaracteristica> InmuebleCaracteristica { get; set; }
        public DbSet<Contacto> Contacto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            // ROLES
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(u => u.IdRol);
                entity.Property(u => u.IdRol).HasColumnName("id_rol");
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100);
            });

            // USUARIOS
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.HasKey(u => u.IdUsuario);

                entity.Property(u => u.IdUsuario).HasColumnName("id_usuario").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100);
                entity.Property(u => u.Correo).HasColumnName("correo").IsRequired().HasMaxLength(100);
                entity.Property(u => u.Contraseña).HasColumnName("contraseña").IsRequired().HasMaxLength(255);
                entity.Property(u => u.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(u => u.IdRol).HasColumnName("id_rol");
                entity.Property(u => u.FechaRegistro).HasColumnName("fecha_registro").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(u => u.Correo).IsUnique();
                entity.HasOne(u => u.Rol).WithMany().HasForeignKey(u => u.IdRol);
            });

            // UBICACIONES
            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.ToTable("ciudades");
                entity.HasKey(u => u.IdCiudad);
                entity.Property(u => u.IdCiudad).HasColumnName("id_ciudad").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100);
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.ToTable("localidad");
                entity.HasKey(u => u.IdLocalidad);
                entity.Property(u => u.IdLocalidad).HasColumnName("id_localidad").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100);
            });

            modelBuilder.Entity<Barrio>(entity =>
            {
                entity.ToTable("barrios");
                entity.HasKey(u => u.IdBarrio);

                entity.Property(u => u.IdBarrio).HasColumnName("id_barrio").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100);
                entity.Property(u => u.IdCiudad).HasColumnName("id_ciudad");
                entity.Property(u => u.IdLocalidad).HasColumnName("id_localidad");

                entity.HasOne(u => u.Ciudad).WithMany().HasForeignKey(u => u.IdCiudad);
                entity.HasOne(u => u.Localidad).WithMany().HasForeignKey(u => u.IdLocalidad);
            });

            // TIPO DE INMUEBLE
            modelBuilder.Entity<TipoInmueble>(entity =>
            {
                entity.ToTable("tipos_inmueble");
                entity.HasKey(u => u.IdTipo);
                entity.Property(u => u.IdTipo).HasColumnName("id_tipo").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(50);
            });

            // ESTADO DE PUBLICACIÓN
            modelBuilder.Entity<EstadoPublicacion>(entity =>
            {
                entity.ToTable("estados_publicacion");
                entity.HasKey(u => u.IdEstado);
                entity.Property(u => u.IdEstado).HasColumnName("id_estado").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(50);
            });

            // INMUEBLE
            modelBuilder.Entity<Inmueble>(entity =>
            {
                entity.ToTable("inmuebles");
                entity.HasKey(u => u.IdInmueble);

                entity.Property(u => u.IdInmueble).HasColumnName("id_inmueble").ValueGeneratedOnAdd();
                entity.Property(u => u.Titulo).HasColumnName("titulo").HasMaxLength(150);
                entity.Property(u => u.Descripcion).HasColumnName("descripcion").HasColumnType("TEXT");
                entity.Property(u => u.Precio).HasColumnName("precio").HasPrecision(12, 2);
                entity.Property(u => u.IdTipo).HasColumnName("id_tipo");
                entity.Property(u => u.Direccion).HasColumnName("direccion").HasMaxLength(200);
                entity.Property(u => u.IdBarrio).HasColumnName("id_barrio");
                entity.Property(u => u.Habitaciones).HasColumnName("habitaciones");
                entity.Property(u => u.Baños).HasColumnName("baños");
                entity.Property(u => u.MetrosCuadrados).HasColumnName("metros_cuadrados");
                entity.Property(u => u.Estrato).HasColumnName("estrato");
                entity.Property(u => u.Latitud).HasColumnName("latitud").HasPrecision(10, 8);
                entity.Property(u => u.Longitud).HasColumnName("longitud").HasPrecision(11, 8);
                entity.Property(u => u.IdUsuario).HasColumnName("id_usuario");
                entity.Property(u => u.IdEstado).HasColumnName("id_estado").HasDefaultValue(1);
                entity.Property(u => u.FechaPublicacion).HasColumnName("fecha_publicacion").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(u => u.TipoInmueble).WithMany().HasForeignKey(u => u.IdTipo);
                entity.HasOne(u => u.Barrio).WithMany().HasForeignKey(u => u.IdBarrio);
                entity.HasOne(u => u.Usuario).WithMany().HasForeignKey(u => u.IdUsuario);
                entity.HasOne(u => u.EstadoPublicacion).WithMany().HasForeignKey(u => u.IdEstado);
            });

            // IMAGEN
            modelBuilder.Entity<Imagen>(entity =>
            {
                entity.ToTable("imagenes");
                entity.HasKey(u => u.IdImagen);

                entity.Property(u => u.IdImagen).HasColumnName("id_imagen").ValueGeneratedOnAdd();
                entity.Property(u => u.Url).HasColumnName("url").HasMaxLength(255);
                entity.Property(u => u.IdInmueble).HasColumnName("id_inmueble");

                entity.HasOne(u => u.Inmueble).WithMany().HasForeignKey(u => u.IdInmueble);
            });

            // CARACTERÍSTICA
            modelBuilder.Entity<Caracteristica>(entity =>
            {
                entity.ToTable("caracteristicas");
                entity.HasKey(u => u.IdCaracteristica);
                entity.Property(u => u.IdCaracteristica).HasColumnName("id_caracteristica").ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100);
            });

            // RELACIÓN N:M (Inmueble <-> Caracteristica)
            modelBuilder.Entity<InmuebleCaracteristica>(entity =>
            {
                entity.ToTable("inmueble_caracteristica");
                entity.HasKey(e => new { e.IdInmueble, e.IdCaracteristica });

                entity.Property(u => u.IdInmueble).HasColumnName("id_inmueble");
                entity.Property(u => u.IdCaracteristica).HasColumnName("id_caracteristica");

                entity.HasOne(u => u.Inmueble).WithMany().HasForeignKey(u => u.IdInmueble);
                entity.HasOne(u => u.Caracteristica).WithMany().HasForeignKey(u => u.IdCaracteristica);
            });

            // CONTACTO
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.ToTable("contactos");
                entity.HasKey(u => u.IdContacto);

                entity.Property(u => u.IdContacto).HasColumnName("id_contacto").ValueGeneratedOnAdd();
                entity.Property(u => u.IdComprador).HasColumnName("id_comprador");
                entity.Property(u => u.IdVendedor).HasColumnName("id_vendedor");
                entity.Property(u => u.IdInmueble).HasColumnName("id_inmueble");
                entity.Property(u => u.Mensaje).HasColumnName("mensaje").HasColumnType("TEXT");
                entity.Property(u => u.Fecha).HasColumnName("fecha").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(u => u.Comprador).WithMany().HasForeignKey(u => u.IdComprador).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(u => u.Vendedor).WithMany().HasForeignKey(u => u.IdVendedor).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(u => u.Inmueble).WithMany().HasForeignKey(u => u.IdInmueble);
            });
        }
    }
}
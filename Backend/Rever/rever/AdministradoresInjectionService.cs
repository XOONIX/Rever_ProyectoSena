using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rever.contexto;
using rever.Repositories;
using rever.Repositories.Interfaces;
using System;


namespace rever
{
    public static class AdministradoresInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            String connectionString = "";
            connectionString = configuration["ConnectionStrings:SQLConnectionStrings"];

            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null)));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICiudadRepository, CiudadRepository>();
            services.AddScoped<ILocalidadRepository, LocalidadRepository>();
            services.AddScoped<IBarrioRepository, BarrioRepository>();
            services.AddScoped<IInmuebleCaracteristicaRepository, InmuebleCaracteristicaRepository>();
            services.AddScoped<ITipoInmuebleRepository, TipoInmuebleRepository>();
            services.AddScoped<IEstadoPublicacionRepository, EstadoPublicacionRepository>();
            services.AddScoped<IInmuebleRepository, InmuebleRepository>();
            services.AddScoped<IImagenRepository, ImagenRepository>();
            services.AddScoped<ICaracteristicaRepository, CaracteristicaRepository>();
            services.AddScoped<IContactoRepository, ContactoRepository>();
            services.AddScoped<IRolRepository, RolRepository>();

            return services;
        }
    }
}
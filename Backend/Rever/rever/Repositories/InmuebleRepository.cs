using Microsoft.EntityFrameworkCore;
using rever.contexto;
using rever.Dtos;
using rever.Models;
using rever.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuProyecto.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace rever.Repositories
{
    public class InmuebleRepository : IInmuebleRepository
    {
        private readonly DatabaseService _context;

        public InmuebleRepository(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Inmueble>> GetInmueble()
        {
            var data = await _context.Inmueble.ToListAsync();
            return data;
        }

        public async Task<Inmueble> GetInmuebleById(int id)
        {
            var data = await _context.Inmueble.FirstOrDefaultAsync(x => x.IdInmueble == id);
            return data;
        }

        public async Task<bool> PostInmueble(Inmueble Inmueble)
        {
            await _context.Inmueble.AddAsync(Inmueble);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutInmueble(Inmueble Inmueble)
        {
            var exist = _context.Inmueble.FirstOrDefault(x => x.IdInmueble == Inmueble.IdInmueble);
            if (exist == null)
            {
                return false;
            }

            exist.Titulo = Inmueble.Titulo;
            exist.Descripcion = Inmueble.Descripcion;
            exist.Precio = Inmueble.Precio;
            exist.IdTipo = Inmueble.IdTipo;
            exist.Direccion = Inmueble.Direccion;
            exist.IdBarrio = Inmueble.IdBarrio;
            exist.Habitaciones = Inmueble.Habitaciones;
            exist.Baños = Inmueble.Baños;
            exist.MetrosCuadrados = Inmueble.MetrosCuadrados;
            exist.Estrato = Inmueble.Estrato;
            exist.Latitud = Inmueble.Latitud;
            exist.Longitud = Inmueble.Longitud;
            exist.IdUsuario = Inmueble.IdUsuario;
            exist.IdEstado = Inmueble.IdEstado;
            _context.Inmueble.Update(Inmueble);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInmueble(Inmueble Inmueble)
        {
            _context.Inmueble.Remove(Inmueble);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<InmuebleListadoDto>> GetListadoAsync()
        {
            return await _context.Inmueble
                .Include(i => i.Barrio).ThenInclude(b => b.Ciudad)
                .Include(i => i.Barrio).ThenInclude(b => b.Localidad)  
                .Include(i => i.TipoInmueble)
                .Include(i => i.ModoTransaccion)
                .Include(i => i.Imagenes)
                .Include(i => i.InmuebleCaracteristicas).ThenInclude(ic => ic.Caracteristica)
                .Select(i => new InmuebleListadoDto
                {
                    IdInmueble = i.IdInmueble,
                    Titulo = i.Titulo,
                    Precio = i.Precio,
                    Ubicacion = i.Barrio.Nombre + ", " + i.Barrio.Ciudad.Nombre,
                    Habitaciones = i.Habitaciones,
                    Banos = i.Baños,
                    MetrosCuadrados = i.MetrosCuadrados,
                    Pisos = i.Pisos,
                    Tipo = i.TipoInmueble.Nombre,
                    Modo = i.ModoTransaccion.Nombre.ToLower(),
                    Ciudad = i.Barrio.Ciudad.Nombre,
                    Localidad = i.Barrio.Localidad.Nombre,
                    Barrio = i.Barrio.Nombre,
                    ImagenUrl = i.Imagenes.Where(img => img.Portada).Select(img => img.Url).FirstOrDefault() ?? i.Imagenes.Select(img => img.Url).FirstOrDefault(),
                    Caracteristicas = i.InmuebleCaracteristicas.Select(ic => ic.Caracteristica.Nombre).ToList(),
                })
                .ToListAsync();
        }

        public async Task<InmuebleDetalleDto?> GetDetalleAsync(int id)
        {
            return await _context.Inmueble
                .Include(i => i.Barrio).ThenInclude(b => b.Ciudad)
                .Include(i => i.TipoInmueble)
                .Include(i => i.ModoTransaccion)
                .Include(i => i.Imagenes)
                .Include(i => i.InmuebleCaracteristicas).ThenInclude(ic => ic.Caracteristica)
                .Include(i => i.Usuario)
                .Where(i => i.IdInmueble == id)
                .Select(i => new InmuebleDetalleDto
                {
                    IdInmueble = i.IdInmueble,
                    Titulo = i.Titulo,
                    Descripcion = i.Descripcion,
                    Precio = i.Precio,
                    Direccion = i.Direccion,
                    Ubicacion = i.Barrio.Nombre + ", " + i.Barrio.Ciudad.Nombre,
                    Habitaciones = i.Habitaciones,
                    Banos = i.Baños,
                    MetrosCuadrados = i.MetrosCuadrados,
                    Pisos = i.Pisos,
                    Estrato = i.Estrato,
                    Latitud = i.Latitud,
                    Longitud = i.Longitud,
                    Tipo = i.TipoInmueble.Nombre,
                    Modo = i.ModoTransaccion.Nombre.ToLower(),
                    Imagenes = i.Imagenes.Select(img => img.Url).ToList(),
                    Caracteristicas = i.InmuebleCaracteristicas.Select(ic => ic.Caracteristica.Nombre).ToList(),
                    IdVendedor = i.IdUsuario,
                    NombreVendedor = i.Usuario.Nombre,
                    TelefonoVendedor = i.Usuario.Telefono,
                    CorreoVendedor = i.Usuario.Correo,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Inmueble?> PostInmuebleCompleto(CrearInmuebleDto dto, int idUsuario)
        {
            var caracIds = dto.CaracteristicasIds.Distinct().ToList();

            // Validar que las características existan (solo lectura, fuera de la transacción)
            if (caracIds.Count > 0)
            {
                var existentes = await _context.Caracteristica
                    .CountAsync(c => caracIds.Contains(c.IdCaracteristica));
                if (existentes != caracIds.Count)
                    throw new ArgumentException("Una o más características no existen.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                // Si hay un reintento, limpia lo que quedó rastreado del intento anterior
                _context.ChangeTracker.Clear();

                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    var inmueble = new Inmueble
                    {
                        Titulo = dto.Titulo.Trim(),
                        Descripcion = dto.Descripcion.Trim(),
                        Precio = dto.Precio,
                        IdTipo = dto.IdTipo,
                        IdModo = dto.IdModo,
                        Direccion = dto.Direccion.Trim(),
                        IdBarrio = dto.IdBarrio,
                        Habitaciones = dto.Habitaciones,
                        Baños = dto.Banos,
                        Pisos = dto.Pisos,
                        MetrosCuadrados = dto.MetrosCuadrados,
                        Estrato = dto.Estrato,
                        Latitud = dto.Latitud,
                        Longitud = dto.Longitud,
                        IdUsuario = idUsuario,
                        IdEstado = 1,
                    };

                    _context.Inmueble.Add(inmueble);
                    await _context.SaveChangesAsync();   // aquí se genera IdInmueble

                    if (caracIds.Count > 0)
                    {
                        _context.InmuebleCaracteristica.AddRange(
                            caracIds.Select(id => new InmuebleCaracteristica
                            {
                                IdInmueble = inmueble.IdInmueble,
                                IdCaracteristica = id
                            }));
                    }

                    var urls = dto.ImagenesUrls
                        .Where(u => !string.IsNullOrWhiteSpace(u))
                        .Select(u => u.Trim())
                        .ToList();

                    if (urls.Count > 0)
                    {
                        _context.Imagen.AddRange(
                            urls.Select((url, i) => new Imagen
                            {
                                IdInmueble = inmueble.IdInmueble,
                                Url = url,
                                Portada = i == 0   // la primera imagen queda como portada
                            }));
                    }

                    await _context.SaveChangesAsync();
                    await tx.CommitAsync();
                    return inmueble;
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }
    }
    
}
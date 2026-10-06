namespace rever.Dtos
{
    public class InmuebleDetalleDto
    {
        public int IdInmueble { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; }       // localidad + ciudad
        public int Habitaciones { get; set; }
        public int Banos { get; set; }
        public int MetrosCuadrados { get; set; }
        public int Pisos { get; set; }
        public int Estrato { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Tipo { get; set; }
        public string Modo { get; set; }
        public List<string> Imagenes { get; set; } = new();
        public List<string> Caracteristicas { get; set; } = new();

        // Datos del vendedor, para la tarjeta de contacto
        public int IdVendedor { get; set; }
        public string NombreVendedor { get; set; }
        public string TelefonoVendedor { get; set; }
        public string CorreoVendedor { get; set; }
    }
}
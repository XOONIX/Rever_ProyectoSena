namespace rever.Dtos
{
    public class InmuebleListadoDto
    {
        public int IdInmueble { get; set; }
        public string Titulo { get; set; }
        public decimal Precio { get; set; }
        public string Ubicacion { get; set; }
        public int Habitaciones { get; set; }
        public int Banos { get; set; }
        public double MetrosCuadrados { get; set; }
        public string Tipo { get; set; }
        public string Modo { get; set; }
        public string ImagenUrl { get; set; }
        public List<string> Caracteristicas { get; set; }
    }
}

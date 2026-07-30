using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Roles
{
    public int IdRol { get; set; }
    public string Nombre { get; set; }
}

public class Usuarios
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public string Telefono { get; set; }
    public int IdRol { get; set; }
    public DateTime FechaRegistro { get; set; }
}

public class Ciudades
{
    public int IdCiudad { get; set; }
    public string Nombre { get; set; }
}

public class Localidades
{
    public int IdLocalidad { get; set; }
    public string Nombre { get; set; }
}

public class Barrios
{
    public int IdBarrio { get; set; }
    public string Nombre { get; set; }
    public int IdCiudad { get; set; }
    public int IdLocalidad { get; set; }
}

public class TipoInmuebles
{
    public int IdTipo { get; set; }
    public string Nombre { get; set; }
}

public class EstadoPublicaciones
{
    public int IdEstado { get; set; }
    public string Nombre { get; set; }
}

public class Inmuebles
{
    public int IdInmueble { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int IdTipo { get; set; }
    public string Direccion { get; set; }
    public int IdBarrio { get; set; }
    public int Habitaciones { get; set; }
    public int Baños { get; set; }
    public int MetrosCuadrados { get; set; }
    public int Estrato { get; set; }
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public int IdUsuario { get; set; }
    public int IdEstado { get; set; }
    public DateTime FechaPublicacion { get; set; }

}

public class Imagenes
{
    public int IdImagen { get; set; }
    public string Url { get; set; }
    public int IdInmueble { get; set; }
}

public class Caracteristicas
{
    public int IdCaracteristica { get; set; }
    public string Nombre { get; set; }
}

public class Contactos
{
    public int IdContacto { get; set; }
    public int IdComprador { get; set; }
    public int IdVendedor { get; set; }
    public int IdInmueble { get; set; }
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
}

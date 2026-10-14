using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rever.Models;

namespace rever.Tests.Helpers
{
    /// <summary>
    /// Utilidades compartidas para simular usuarios autenticados en los tests de controladores.
    /// SUPOSICIÓN: las extensiones User.ObtenerIdUsuario() / EsAdministrador() / EsDueñoOAdmin()
    /// leen el id desde ClaimTypes.NameIdentifier y el rol de administrador es "1".
    /// Si tus extensiones usan otro claim, cámbialo solo aquí.
    /// </summary>
    public static class ControllerTestHelper
    {
        public const string RolAdmin = "1";
        public const string RolUsuario = "2";

        public static ClaimsPrincipal CrearUsuario(int id, string rol = RolUsuario)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, $"Usuario{id}"),
                new Claim(ClaimTypes.Role, rol)
            }, "TestAuthentication"));
        }

        // Usuario autenticado pero SIN claim de id (para probar "no se pudo verificar la identidad")
        public static ClaimsPrincipal CrearUsuarioSinId(string rol = RolUsuario)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "UsuarioSinId"),
                new Claim(ClaimTypes.Role, rol)
            }, "TestAuthentication"));
        }

        // Inmueble tiene miembros 'required'; aquí se llenan todos y solo varían id y dueño.
        public static Inmueble CrearInmueble(int idUsuario, int idInmueble = 1)
        {
            return new Inmueble
            {
                IdInmueble = idInmueble,
                IdUsuario = idUsuario,
                Titulo = "Inmueble de prueba",
                Descripcion = "Descripción de prueba",
                Direccion = "Calle 1 # 2-3",
                IdLocalidad = 1,
                Habitaciones = 1,
                Baños = 1,
                MetrosCuadrados = 50,
                Latitud = 0,
                Longitud = 0
            };
        }

        public static void AsignarUsuario(ControllerBase controller, ClaimsPrincipal user)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        // Contexto sin usuario autenticado
        public static void AsignarAnonimo(ControllerBase controller)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }
    }
}

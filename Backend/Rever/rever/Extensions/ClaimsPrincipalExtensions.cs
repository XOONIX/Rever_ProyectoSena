using System.Security.Claims;

namespace rever
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? ObtenerIdUsuario(this ClaimsPrincipal user)
        {
            if (user == null) return null;

            var claim = user.FindFirst("idUsuario")
                     ?? user.FindFirst(ClaimTypes.NameIdentifier)
                     ?? user.FindFirst("sub")
                     ?? user.FindFirst("id");

            if (claim != null && int.TryParse(claim.Value, out int id))
            {
                return id;
            }

            return null;
        }

        public static bool EsAdministrador(this ClaimsPrincipal user)
        {
            return user.IsInRole("1"); // el claim Role es el id numérico del rol
        }


        /// True si el usuario autenticado es el dueño del recurso (comparando idPropietario)
        /// o si es administrador. False si no hay token válido.
        public static bool EsDueñoOAdmin(this ClaimsPrincipal user, int idPropietario)
        {
            var idUsuarioToken = user.ObtenerIdUsuario();
            if (!idUsuarioToken.HasValue)
            {
                return false;
            }

            return idUsuarioToken.Value == idPropietario || user.EsAdministrador();
        }
    }
}
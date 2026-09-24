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
    }
}

using System.Linq;
using Microsoft.AspNetCore.Http;

namespace JwtAuth.Api.CustomAuth
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type == claimName && c.Value == claimValue);
                //context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(",").Contains(claimValue));
        }
    }
}
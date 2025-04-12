using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PosDesktop.Helpers;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        return jwt.Claims;
    }
}
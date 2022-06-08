using Manager.Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Web.Administration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Manager.Api.Auth
{
    public class AuthToken
    {
        public static class JwtAuth
        {
            public static string GenerateToken(Usuario user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Settings.key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, user.Email.ToString())

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }

    public static class Settings 
    {
        public static string key = "Yaw019OvEDUafphyWk9vrmHu8abbRpr8WjVHEKtXJd030ifIrqNh4JrpgOtFZLV";
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreWebApiBoilerPlate.Core
{
    public class JWT
    {
        public static string GenerateToken(Dictionary<string, string> claimsToBeAdded,  string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var expiresAt = DateTime.Now.AddDays(30);

            var claimsIdentity = new ClaimsIdentity();
            foreach (var item in claimsToBeAdded)
            {
                claimsIdentity.AddClaim(new Claim(item.Key, item.Value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

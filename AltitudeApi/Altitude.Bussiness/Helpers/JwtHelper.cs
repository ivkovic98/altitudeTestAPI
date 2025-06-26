using Altitude.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

     public async Task<List<Claim>> GetClaims(User user)
     {
        var claims = new List<Claim>
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.UserRole.ToString()),
        new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
        new Claim(ClaimTypes.Surname, user.LastName ?? ""),
        new Claim("username", user.Username ?? ""),
        new Claim("phone_number", user.Number ?? ""),
         };

            await Task.CompletedTask;
            return claims;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims, bool rememberMe)
        {
            var expires = rememberMe
                ? DateTime.UtcNow.AddYears(10)
                : DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"]));

            return new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
        }

    }
}

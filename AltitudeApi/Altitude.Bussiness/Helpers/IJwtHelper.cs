using Altitude.Data.Entities;
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
    public interface IJwtHelper
    {
        Task<List<Claim>> GetClaims(User user);
        SigningCredentials GetSigningCredentials();
        JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims, bool rememberMe);

    }
}

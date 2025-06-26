using Altitude.Bussiness.Helpers;
using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models.Login;
using Altitude.Data.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepository, IJwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResponseModel?> LoginAsync(LoginModel model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return null;

            var credentials = _jwtHelper.GetSigningCredentials();
            var claims =  await _jwtHelper.GetClaims(user);
            var token = _jwtHelper.GenerateTokenOptions(credentials, claims, model.RememberMe);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponseModel { Token = tokenString };
        }
    }
}

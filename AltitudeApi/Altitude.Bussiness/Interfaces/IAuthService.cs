using Altitude.Bussiness.Models.Login;
using Altitude.Bussiness.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseModel?> LoginAsync(LoginModel model);

    }
}

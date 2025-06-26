using Altitude.Bussiness.Models;
using Altitude.Bussiness.Models.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseModel>> GetAllUsersAsync();
        Task<UserResponseModel?> GetUserByIdAsync(Guid id);
        Task<UserResponseModel?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserRegisterModel model);
        Task UpdateUserAsync(Guid id, UserRegisterModel model);
        Task DeleteUserAsync(Guid id);
        Task<UserResponseModel?> GetCurrentUserProfileAsync(Guid userId);
        Task<bool> UpdateUserProfileImageAsync(UpdateUserPhotoModel model);
    }
}
 
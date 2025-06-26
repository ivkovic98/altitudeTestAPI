using Altitude.Bussiness.Interface;
using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models;
using Altitude.Bussiness.Models.User;
using Altitude.Data.Entities;
using Altitude.Data.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageUploadService _imageUploadService;

        public UserService(IUserRepository userRepository, IMapper mapper, IImageUploadService imageUploadService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageUploadService = imageUploadService;
        }

        public async Task AddUserAsync(UserRegisterModel model)
        {
            User newUser = _mapper.Map<User>(model);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            newUser.UserRole = Common.Enums.UserRole.Customer;
          

            await _userRepository.AddAsync(newUser);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null )
                return;

            user.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllActiveAsync();
            return _mapper.Map<IEnumerable<UserResponseModel>>(users);
        }

        public async Task<UserResponseModel?> GetCurrentUserProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.IsDeleted)
                return null;

            return _mapper.Map<UserResponseModel>(user);
        }

        public async Task<UserResponseModel?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user == null ? null : _mapper.Map<UserResponseModel>(user);
        }

        public async Task<UserResponseModel?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserResponseModel>(user);
        }

        public async Task UpdateUserAsync(Guid id, UserRegisterModel model)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Username = model.Username;
            user.Number = model.Number;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            if (model.ProfileImageUrl != null && model.ProfileImageUrl.Length > 0)
            {
                var imageUrl = await _imageUploadService.UploadImageAsync(model.ProfileImageUrl, "users");
                user.ProfileImageUrl = imageUrl;
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> UpdateUserProfileImageAsync(UpdateUserPhotoModel model)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(model.UserId));
            if (user == null || user.IsDeleted)
                return false;

            if (model.ProfilePhoto != null && model.ProfilePhoto.Length > 0)
            {
                if (model.ProfilePhoto.Length > 5 * 1024 * 1024)
                    throw new ArgumentException("File size must be less than 5MB");

                var imageUrl = await _imageUploadService.UploadImageAsync(model.ProfilePhoto, "users");
                user.ProfileImageUrl = imageUrl;
                await _userRepository.UpdateAsync(user);
                return true;
            }

            return false;
        }
    }
}

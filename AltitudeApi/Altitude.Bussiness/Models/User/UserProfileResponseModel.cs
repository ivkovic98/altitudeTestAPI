using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Models.User
{
    public class UserProfileResponseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;  
        public string? ProfileImageUrl { get; set; }
    }
}

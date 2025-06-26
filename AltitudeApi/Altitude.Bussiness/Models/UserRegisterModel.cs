using Altitude.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Models
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;    
        public string Number { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public IFormFile? ProfileImageUrl { get; set; }


    }
}

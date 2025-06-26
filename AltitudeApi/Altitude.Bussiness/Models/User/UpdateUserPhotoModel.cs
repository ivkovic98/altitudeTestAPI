using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Models.User
{
    public class UpdateUserPhotoModel
    {
        public string UserId { get; set; }
        public IFormFile? ProfilePhoto { get; set; }

    }
}

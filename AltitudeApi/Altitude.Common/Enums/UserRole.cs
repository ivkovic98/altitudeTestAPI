using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Common.Enums
{
    public enum UserRole
    {
        Admin = 1,
        Customer = 2,
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 2,
        Banned = 3
    }

    public enum LogLevel
    {
        Info = 1,
        Warning = 2,
        Error = 3,
        Critical = 4
    }
}

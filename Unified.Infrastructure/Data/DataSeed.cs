using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Infrastructure.Data
{
    public static class DataSeed
    {
        // Roles
        public const string AdminRole = "Admin";
        public const string HrRole = "HR";
        public const string ItRole = "IT";
        public const string AssisstantRole = "Assisstant";

        // Default System Users
        public const string AdminUserName = "admin@example.com";
        public const string HrUserName = "hr@example.com";
        public const string TechnicianUserName = "technician@example.com";
        public const string AssisstantUserName = "assisstant@example.com";

        //Login Attempts
        public const int MaximumLoginAttempts = 3;
    }
}

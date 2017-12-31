using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bursa.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }

        public int UserType { get; set; }

        public long Permissions { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
        public string Login { get; set; }
    }
}

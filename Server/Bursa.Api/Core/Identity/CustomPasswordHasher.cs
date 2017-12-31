using Bursa.Api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bursa.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(ApplicationUser user, string password)
        {
            return password;
           // throw new NotImplementedException();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            
            return  hashedPassword.Equals(providedPassword)? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
           
        }
    }
}

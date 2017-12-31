using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Toolkit.Extensions;

namespace Bursa.Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClaimsPrincipleHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int GetAccountId(this ClaimsPrincipal user )
        {
            return user.Claims.FirstOrDefault(x => x.Type == "AccountId").Value.StringToInt();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bursa.Api
{
    /// <summary>
    /// custom normilizer to prevent cpitalize
    /// </summary>
    public class CustomNormalizer : ILookupNormalizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Normalize(string key)
        {
            return key;
        }
    }
}

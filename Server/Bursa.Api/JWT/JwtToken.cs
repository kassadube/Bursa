using System;
using System.IdentityModel.Tokens.Jwt;

namespace Bursa.Api
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JwtToken
    {
        private JwtSecurityToken token;

        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ValidTo => token.ValidTo;
        /// <summary>
        /// 
        /// </summary>
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}

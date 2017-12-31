using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bursa.Api.Models;
using Bursa.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Bursa.Api.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Bursa.Api.Controllers
{
   /// <summary>
   /// 
   /// </summary>
    [Authorize]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="logger"></param>
        public TokenController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<TokenController> logger)
        {
              _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]LoginInputModel inputModel)
        {
            _logger.LogInformation("TRY LOGIN TOKEN");
            
            var result = await _signInManager.PasswordSignInAsync
                (inputModel.Username, inputModel.Password, false,  false);
            
               if (!result.Succeeded)
                {
                    _logger.LogInformation("User NOT logged in.");
                    return Unauthorized();
                }


            var resUser = await _userManager.FindByNameAsync(inputModel.Username);
              
            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("Kassadube-secret-key"))
                                .AddSubject(resUser.Name)
                                .AddIssuer("Kassadube.Security.Bearer")
                                .AddAudience("Kassadube.Security.Bearer")
                                .AddClaim("AccountId", resUser.AccountId.ToString())
                                .AddClaim("UserId", resUser.UserId.ToString())
                                .AddClaim("UserType", resUser.UserType.ToString())
                                .AddClaim("Permissions", resUser.Permissions.ToString())
                                .AddExpiry(120)
                                .Build();

            //return Ok(token);
            return Ok(new { token = token.Value, expire = DateTime.Now.AddMinutes(120) } );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("token/test")]
        [HttpGet]        
        public IActionResult Test()
        {
            //return Content("List of Movies");

            var dict = new Dictionary<string, string>();
            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));
            dict.Add("AccountIdTest", HttpContext.User.GetAccountId().ToString());
            return Ok(dict);
        }

    }
}

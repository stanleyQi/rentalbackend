using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using rentalbackend.Dto;
using rentalbackend.Entities;
using rentalbackend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rentalbackend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController:ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        //private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    return await JwtHelper.GenerateJwtToken(model.Email, appUser, _configuration);
                }

                return new { code = "00008", message = "Operation errors", error="An operation error happened." };
            }
            catch (Exception)
            {

                return new { code = "00007", message = "System errors", error = "An system error happened." };
            }
            
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return await JwtHelper.GenerateJwtToken(model.Email, user, _configuration);
                }

                return new { code = "00008", message = "Operation errors", error = "An operation error happened." };
            }
            catch (Exception e)
            {
                return new { code = "00007", message = "System errors", error = "An system error happened." };
            }
            
        }
    }
}

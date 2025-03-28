﻿using ECommerce.IdentityServer.Dtos;
using ECommerce.IdentityServer.Models;
using ECommerce.IdentityServer.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {

            ApplicationUser currentUser = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (currentUser is null)
                return BadRequest("Kullanici adi hatalidir.");

            userLoginDto.Username = currentUser.UserName;

            var checkPassword = await _userManager.CheckPasswordAsync(currentUser, userLoginDto.Password);
            if (!checkPassword)
                return BadRequest("Şifre hatalidir.");

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);

            if (result.Succeeded)
            {
                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                model.UserName = userLoginDto.Username;
                model.Id = currentUser.Id;
                model.FullName = $"{currentUser.Name} {currentUser.Surname}";
                
                var token = JwtTokenGenerator.GenerateToken(model);
                return Ok(token);
            }

            return BadRequest("Giriş Başarısız!");

            
        }



		[HttpGet("FindUserName")]
		public async Task<IActionResult> FindUserName(string email)
		{
			ApplicationUser currentUser = await _userManager.FindByEmailAsync(email);

			if (currentUser is null)
				return BadRequest("Kullanıcı adı hatalıdır.");

			return Ok(currentUser.UserName);  
		}









	}
}

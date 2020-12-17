using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelPackerAPI.Models;
using TravelPackerAPI.Models.DTOS;
using TravelPackerAPI.Models.RepositoryInterfaces;

namespace TravelPackerAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[ApiConventionType(typeof(DefaultApiConventions))]
	public class AccountController : ControllerBase {

		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;

		public AccountController(
			SignInManager<IdentityUser> signInManager,
		  UserManager<IdentityUser> userManager,
		  IUserRepository userRepository,
		  IConfiguration config) {
			_signInManager = signInManager;
			_userManager = userManager;
			_userRepository = userRepository;
			_config = config;
		}


		/// <summary>
		/// Get bearertoken
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("[action]")]
		public async Task<ActionResult<string>> Login(LoginDTO dto) {
			var user = await _userManager.FindByEmailAsync(dto.Email);

			if (user != null) {
				var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

				if (result.Succeeded) {
					string token = GetToken(user);
					return Created("", token); //returns only the token                    
				}
			}
			return BadRequest();
		}

		[AllowAnonymous]
		[HttpPost("[action]")]
		public async Task<ActionResult<String>> Register(RegisterDTO dto) {
			IdentityUser identUser = new IdentityUser { UserName = dto.FirstName, Email = dto.Email };
			User user = new User(dto.FirstName, dto.LastName, dto.Email);

			var result = await _userManager.CreateAsync(identUser, dto.Password);

			if (result.Succeeded) {
				_userRepository.Add(user);
				_userRepository.SaveChanges();
				string token = GetToken(identUser);
				return Created("", token);
			}
			return BadRequest();
		}

		private string GetToken(IdentityUser user) {
			// Create the token
			var claims = new[]
			{
			  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			  new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
			  null, null,
			  claims,
			  expires: DateTime.Now.AddMinutes(30),
			  signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

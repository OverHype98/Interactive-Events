using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Application.Mail;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IMailSender _mailSender;
		private readonly UserManager<AppUser> _userManager;
		private readonly TokenServices _tokenServices;
		public AccountController(UserManager<AppUser> userManager,
		SignInManager<AppUser> signInManager,
		IMailSender mailSender,
		TokenServices tokenServices)
		{
			_tokenServices = tokenServices;
			_userManager = userManager;
			_signInManager = signInManager;
			_mailSender = mailSender;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{

			var user = await _userManager.Users.Include(p => p.Photos)
			 .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

			if (user == null) return Unauthorized();

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (result.Succeeded)
			{
				return CreateUserObject(user);

			}
			return Unauthorized();
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
			{
				ModelState.AddModelError("email", "Email taken");
				return ValidationProblem();
			}

			if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
			{
				ModelState.AddModelError("username", "Username taken");
				return ValidationProblem();
			}

			var user = new AppUser
			{
				DisplayName = registerDto.DisplayName,
				Email = registerDto.Email,
				UserName = registerDto.UserName,
				Bio = ""
			};

			var result = await _userManager.CreateAsync(user, registerDto.Password);

			if (result.Succeeded)
			{
				_mailSender.SendAccountCreatedEmail(user.DisplayName, user.Email);
				return CreateUserObject(user);
			}

			return BadRequest("Problem registering user");
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{

			var user = await _userManager.Users.Include(p => p.Photos)
			  .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

			return CreateUserObject(user);
		}
		private UserDto CreateUserObject(AppUser user)
		{
			return new UserDto
			{
				DisplayName = user?.DisplayName,
				Image = user?.Photos?.FirstOrDefault(p => p.IsMain)?.Url,
				Token = _tokenServices.CreateToken(user),
				UserName = user.UserName
			};
		}
	}
}
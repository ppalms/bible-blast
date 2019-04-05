using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BibleBlast.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager,
            IUserRepository userRepo, IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var user = _mapper.Map<User>(request);

            var userResult = await _userManager.CreateAsync(user, request.Password);
            if (!userResult.Succeeded)
            {
                return BadRequest(userResult.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, UserRoles.Member);
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }

            var newUser = await _userRepo.GetUser(user.Id, true);
            var newUserDetail = _mapper.Map<UserDetail>(newUser);

            return CreatedAtRoute("GetUser", new { controller = "Users", id = newUser.Id }, newUserDetail);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _userManager.Users.IgnoreQueryFilters()
                .Include(x => x.Organization)
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                var userInfo = _mapper.Map<UserDetail>(user);

                return Ok(new { token = GenerateJwtToken(user).Result, user = userInfo });
            }

            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            // Who is this person? Claims describe the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("organizationId", user.Organization?.Id.ToString() ?? string.Empty),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Server needs to sign the token to prove its validity
            // 1. Create a security key using the secret we know
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            // 2. Create signing credentials containing the encrypted key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create the token to send back to the client which they can subsequently use
            // to make API requests
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

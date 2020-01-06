using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MultiVendorAPI.Data;
using MultiVendorAPI.Dtos;
using MultiVendorAPI.Models;

namespace MultiVendorAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuth _repo;
        private readonly IConfiguration _config;

        public AuthController(DataContext context, IAuth repo, IConfiguration config)
        {
            _context = context;
            _config = config;
            _repo = repo;
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserToLogin userLoginDto)
        {
            var userFromRepo = await _repo.Login(userLoginDto.Email, userLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized("No Record Found!");
            }



            else
            {
                var claim = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Name),
                    new Claim(ClaimTypes.Email, userFromRepo.Email),
                    new Claim(ClaimTypes.Role, userFromRepo.Level)
                };

                //Create Token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claim),
                    Expires = DateTime.Now.AddDays(60),
                    SigningCredentials = creds
                };


                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);


                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });


            }
        }





        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Users users)
        {
            users.Email = users.Email.ToLower();

            if (await _repo.UserExists(users.Email))
            {
                return BadRequest("Email already taken");
            }

            else
            {
                var createdUser = await _repo.Register(users, users.Password);

                return StatusCode(201);
            }

        }




        [HttpPost("register/check")]
        public async Task<IActionResult> checkRegister([FromBody]Users users)
        {
            users.Email = users.Email.ToLower();

            if (await _repo.UserExists(users.Email))
            {
                return BadRequest("Email already taken");
            }

            else
            {
                return Ok();
            }

        }


    }
}
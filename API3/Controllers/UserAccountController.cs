using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API3.Models;
using JwtExampleIdentity.Abstract;
using LoginAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using vcn.Entities;

namespace API3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly ITokenService _tokenService;

        public UserAccountController(EFContext context , UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager , ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("OutputAccount/{id}")]
        public async Task<ActionResult<string>> GetAccount(string id)
        {
            


        }

        private string CreateTokenAsync(UserAccount user)
        {
            var now = DateTime.UtcNow;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key-example"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);



            // Generate the jwt token
            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: now.Add(TimeSpan.FromDays(1))
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]AddAccountViewModel model)
        {
            UserAccount user = await _userManager.FindByNameAsync(model.Name);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (!result.Succeeded)
                {
                    return BadRequest("Bad data");
                }
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                };
                foreach (var item in await _userManager.GetRolesAsync(user))
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                return Ok(_tokenService.GenerateAccessToken(claims));
            }
            return BadRequest("Bad password or login");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AddAccountViewModel model)
        {
            UserAccount user = new UserAccount()
            {
                 = model.Login,
                 = model.PhoneNumber,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Неправильно введені дані");
        }

        // PUT: api/category/edit/5
        [HttpPut("EditAccount")]
        public async Task<IActionResult> EditLoginAccount([FromBody]editInnerAccountViewModel model)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);

            var userAccount = _context.UserAccounts.FirstOrDefault(t => (t.Id) == user.Id);
            if (userAccount != null)
            {
                userAccount.Name = model.Name;
                userAccount.UserName = model.Name;
                userAccount.PasswordHash = model.Name;
                userAccount.Image = model.Image;
                userAccount.PhoneNumber = model.PhoneNumber;
                userAccount.Email = model.Email;

                _context.SaveChanges();
                return Content("Completed");
            }
            return Content("Error");
        }

        // DELETE: api/category/delete
        [Authorize]
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);

            var loginAcc = _context.UserAccounts.FirstOrDefault(t => t.Id == user.Id);
            if (loginAcc != null)
            {
                _context.Remove(loginAcc);
                _context.SaveChanges();
                return Content("Your account is deleted!");
            }
            return Content("Action not happened!");
        }
    }

}

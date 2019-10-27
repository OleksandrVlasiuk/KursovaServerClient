using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API3.Models;
using LoginAPI.Identity;
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
        private readonly UserManager<ApplicationLogin> _userManager;
        private readonly SignInManager<ApplicationLogin> _signInManager;

        public UserAccountController(EFContext context , UserManager<ApplicationLogin> userManager, SignInManager<ApplicationLogin> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(string login,string password)
        {
            ApplicationLogin user = await _userManager.FindByNameAsync(login);
            var resault = await _signInManager.PasswordSignInAsync(user, password , false, false);
            if (!resault.Succeeded)
            {
                return "Not faithful login or password";

            }
            return CreateTokenAsync(user);
        }

        private string CreateTokenAsync(ApplicationLogin user)
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

        // POST: api/category/add
        [HttpPost("addAccount")]
        public ContentResult AddAccount([FromBody]AddAccountViewModel model)
        {
            try
            {
                UserAccount userAccount = new UserAccount()
                {
                    Name = model.Name,
                    Description = model.Description,
                    UserName = model.Login,
                    PasswordHash = model.Password
                };

                _context.UserAccounts.Add(userAccount);
                _context.SaveChanges();

                return Content("Successfully added");
            }
            catch (Exception ex)
            {
                return Content("Error:" + ex.Message);
            }
        }

        // PUT: api/category/edit/5
        [HttpPut("edit/{id}")]
        public ContentResult EditLoginAccount(int id, [FromBody]editInnerAccountViewModel model)
        {
            var userAccount = _context.UserAccounts.FirstOrDefault(t => Convert.ToInt32(t.Id) == id);
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

        [HttpDelete("delete/{id}")]
        public ContentResult DeleteAccount(int id)
        {
            var loginAcc = _context.UserAccounts.FirstOrDefault(t => Convert.ToInt32(t.Id) == id);
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

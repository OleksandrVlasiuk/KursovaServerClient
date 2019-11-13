using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API3.Models;
using JwtExampleIdentity.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public UserAccountController(
            EFContext context,
            UserManager<UserAccount> userManager, 
            SignInManager<UserAccount> signInManager , 
            ITokenService tokenService , 
            IHostingEnvironment env, 
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpGet("getIcons")]
        public ContentResult GetFriendIcons(List<string>fr)
        {
            List<UserAccount> users = new List<UserAccount>();
            foreach (var t in fr)
            {
               users.Add( _context.Users.FirstOrDefault(f=>f.Id==t));
            }
            string json = JsonConvert.SerializeObject(users);
            return Content(json, "application/json");

        }

        [Authorize]
        [HttpGet("OutputAccount")]
        public async Task<IActionResult> GetAccount()
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            string json = JsonConvert.SerializeObject(user);
            return Content(json,"application/json");
           
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
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            try
            {
                UserAccount user = await _userManager.FindByNameAsync(model.Login);
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

                    return Ok(_tokenService.GenerateAccessToken(claims));
                }
                return BadRequest("Bad password or login");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AddAccountViewModel model)
        {
            UserAccount user = new UserAccount()
            {
                UserName = model.Login,
                Name = model.Name,
                PhoneNumber = model.TelephoneNumber,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Bad request(FromRegistr)");
        }

        // PUT: api/category/edit/5
        [Authorize]
        [HttpPut("EditAccount")]
        public async Task<IActionResult> EditLoginAccount([FromBody]editInnerAccountViewModel model)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);

            var userAccount = _context.Users.FirstOrDefault(t => (t.Id) == user.Id);
            if (model.Image!=null) {
                try
                {
                    string nameOfImage = string.Empty;
                    if (model.Image != null)
                    {
                        string directory = _env.ContentRootPath;
                        string path = Path.Combine(directory, "Content", _configuration["ProductImages"]);
                        byte[] imageBytes = Convert.FromBase64String(model.Image);
                        using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                        {
                            var image = Image.FromStream(ms);
                            nameOfImage = Path.GetRandomFileName() + ".jpg";
                            string pathToFile = Path.Combine(path, nameOfImage);
                            image.Save(pathToFile, ImageFormat.Jpeg);


                        }
                        user.Image = nameOfImage;
                        user.Name = model.Name;
                        user.Description = model.Description;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        _context.SaveChanges();
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

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

            var loginAcc = _context.Users.FirstOrDefault(t => t.Id == user.Id);
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

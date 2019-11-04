using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using vcn.Entities;

namespace API3.Controllers
{
    public class PostController : Controller
    {
        private readonly EFContext _context;
        private readonly UserManager<UserAccount> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public PostController(EFContext context,UserManager<UserAccount> userManager,IHostingEnvironment env,IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
            _configuration = configuration;
        }

        // POST api/values
        [Authorize]
        [HttpPost("AddPost")]
        public async Task<IActionResult> Post([FromBody]PostModel model)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            
            try
            {
                string nameOfImage = string.Empty;
                if (model.File != null)
                {
                    string directory = _env.ContentRootPath;
                    string path = Path.Combine(directory, "Content", _configuration["ProductImages"]);
                    byte[] imageBytes = Convert.FromBase64String(model.File);
                    using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        var image = Image.FromStream(ms);
                        nameOfImage = Path.GetRandomFileName() + ".jpg";
                        string pathToFile = Path.Combine(path, nameOfImage);
                        image.Save(pathToFile, ImageFormat.Jpeg);

                    }

                }
                Post post = new Post()
                {
                    Id = model.Id,
                    File = nameOfImage,
                    Likes = model.Likes,
                    UserAccount_id = model.UserAccount_id
                };
                _context.Posts.Add(post);

 


            _context.Posts.Add(post);
            _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/values/5
        [Authorize]
        [HttpDelete("DeletePost/{postid}")]
        public async Task<IActionResult> Delete(int postid)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);

            if (_context.Posts.FirstOrDefault(t => t.Id == postid).UserAccount_id == user.Id)
            {
                var post = _context.Posts.FirstOrDefault(t => t.Id == postid);
                if (post != null)
                {
                    _context.Posts.Remove(post);
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}
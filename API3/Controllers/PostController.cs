using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Identity;
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
        private readonly UserManager<ApplicationLogin> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public PostController(EFContext context,UserManager<ApplicationLogin> userManager,IHostingEnvironment env,IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
            _configuration = configuration;
        }

        // GET api/values
        [Authorize]
        [HttpGet("GetPostss")]
        public async Task<IActionResult> Get()
        {
            ApplicationLogin login = await _userManager.FindByNameAsync(this.User.Identity.Name);
            List<Post> posts = _context.Posts.Where(t => t.UserAccountId == id).ToList();
            string json = JsonConvert.SerializeObject(comments);
            return Content(json, "application/json");
        }



        // POST api/values
        [HttpPost("AddPost")]
        public async Task<IActionResult> Post([FromBody]ModelPost post)
        {
            ApplicationLogin user = await _userManager.FindByNameAsync(this.User.Identity.Name);
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

                }
                Product product = new Product()
                {
                    Name = model.Name,
                    Image = nameOfImage
                };
                _context.Products.Add(product);

 


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
        [HttpDelete("DeletePost/{Postid}")]
        public void Delete(int postid)
        {
            if (_context.Comments.FirstOrDefault(t => t.Id == Commentid).UserAccountId == id)
            {
                var comment = _context.Comments.FirstOrDefault(t => t.Id == Commentid);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    _context.SaveChanges();
                }
            }
        }
    }
}
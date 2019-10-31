using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API3.Entities;
using API3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vcn.Entities;

namespace API3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<UserAccount> _userManager;

        public CommentController(EFContext context,UserManager<UserAccount>userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET api/values
        [Authorize]
        [HttpGet("GetComment")]
        public async Task<IActionResult> Get()
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            List<Comments> comments = _context.Comments.Where(t => t.UserAccountId == user.Id).ToList();
            string json = JsonConvert.SerializeObject(comments);
            return Content(json, "application/json");
        }



        // POST api/values

        [HttpPost("AddComment")]
        public async Task<IActionResult> Post([FromBody]CommentModel model)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            Comments comments = new Comments()
            {
                Info = model.Info,
                UserAccountId = user.Id,
                PostId = model.PostId
            };
            _context.Comments.Add(comments);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/values/5
        [Authorize]
        [HttpPut("EditMessage/{commentId}")]
        public async Task<IActionResult> Put(int commentId, string value)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            if (_context.Messages.FirstOrDefault(t => t.Id == commentId).UserAccount_id == user.Id)
            {
                var comments = _context.Comments.FirstOrDefault(t => t.Id == commentId);
                if (comments != null)
                {
                    comments.Info = value;
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("DeleteMessage/{Commentid}")]
        public async Task<IActionResult> Delete(int Commentid)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            if (_context.Comments.FirstOrDefault(t => t.Id == Commentid).UserAccountId == user.Id)
            {
                var comment = _context.Comments.FirstOrDefault(t => t.Id == Commentid);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }

    }
}

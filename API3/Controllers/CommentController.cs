using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CommentController(EFContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet("GetComments/{id}")]
        public ContentResult Get(string id)
        {
            List<Comments> comments = _context.Comments.Where(t => t.UserAccountId == id).ToList();
            string json = JsonConvert.SerializeObject(comments);
            return Content(json, "application/json");
        }

   

        // POST api/values
        [HttpPost("AddComment")]
        public void Post(Comments Comment)
        {
                _context.Comments.Add(Comment);
                _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("EditComment/{id}/{commentId}")]
        public void Put(string id,int commentId,string value)
        {
            if (_context.Comments.FirstOrDefault(t => t.Id == commentId).UserAccountId == id)
            {
                var comment = _context.Comments.FirstOrDefault(t => t.Id == commentId);
                if (comment != null)
                {
                    comment.Info = value;
                    _context.SaveChanges();
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("DeleteComment/{id}/{Commentid}")]
        public void Delete(string id ,int Commentid)
        {
            if (_context.Comments.FirstOrDefault(t=>t.Id==Commentid).UserAccountId == id) {
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

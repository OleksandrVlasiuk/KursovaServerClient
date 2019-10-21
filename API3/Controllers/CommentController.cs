﻿using System;
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
        [HttpGet("GetComments")]
        public ContentResult Get()
        {
            List<Comments> comments = _context.Comments.ToList();
            string json = JsonConvert.SerializeObject(comments);
            return Content(json, "application/json");
        }

   

        // POST api/values
        [HttpPost]
        public void Post(Comments Comment)
        {
                _context.Comments.Add(Comment);
                _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id,string value)
        {
            var comment = _context.Comments.FirstOrDefault(t => t.Id == id);
            if (comment != null)
            {
                comment.Info = value;
                _context.SaveChanges();
            }    
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var comment = _context.Comments.FirstOrDefault(t => t.Id == id);
            if (comment != null)
            {
                _context.Remove(comment);
                _context.SaveChanges();
            }
        }
    }
}

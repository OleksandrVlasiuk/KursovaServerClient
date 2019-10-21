using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API3.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vcn.Entities;

namespace API3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly EFContext _context;

        public MessageController(EFContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet("GetMessages")]
        public ContentResult Get()
        {
            List<Message> messages = _context.Messages.ToList();
            string json = JsonConvert.SerializeObject(messages);
            return Content(json, "application/json");
        }



        // POST api/values
        [HttpPost]
        public void Post(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
            var message = _context.Messages.FirstOrDefault(t => t.Id == id);
            if (message != null)
            {
                message.About = value;
                _context.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var message = _context.Messages.FirstOrDefault(t => t.Id == id);
            if (message != null)
            {
                _context.Remove(message);
                _context.SaveChanges();
            }
        }
    }
}

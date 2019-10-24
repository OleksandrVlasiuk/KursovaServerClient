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
        [HttpGet("GetMessages/{id}")]
        public ContentResult Get(string id)
        {
            List<Message> messages = _context.Messages.Where(t => t.UserAccount_id == id).ToList();
            string json = JsonConvert.SerializeObject(messages);
            return Content(json, "application/json");
        }



        // POST api/values
        [HttpPost("AddMessage")]
        public void Post(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("EditMessage/{id}/{messageId}")]
        public void Put(string id, int messageId, string value)
        {
            if (_context.Messages.FirstOrDefault(t => t.Id == messageId).UserAccount_id == id)
            {
                var message = _context.Messages.FirstOrDefault(t => t.Id == messageId);
                if (message != null)
                {
                    message.About = value;
                    _context.SaveChanges();
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("DeleteMessage/{id}/{Messageid}")]
        public void Delete(string id, int Messageid)
        {
            if (_context.Messages.FirstOrDefault(t => t.Id == Messageid).UserAccount_id == id)
            {
                var message = _context.Messages.FirstOrDefault(t => t.Id == Messageid);
                if (message != null)
                {
                    _context.Messages.Remove(message);
                    _context.SaveChanges();
                }
            }
        }
    }
}

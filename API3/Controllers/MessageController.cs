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
    public class MessageController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<UserAccount> _userManager;


        public MessageController(EFContext context,UserManager<UserAccount>userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET api/values
        [Authorize]
        [HttpGet("GetMessages")]
        public async Task<IActionResult>  Get()
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            List<Message> messages = _context.Messages.Where(t => t.UserAccount_id == user.Id).ToList();
            string json = JsonConvert.SerializeObject(messages);
            return Content(json, "application/json");
        }



        // POST api/values
        [Authorize]   
        [HttpPost("AddMessage")]
        public async Task<IActionResult> Post([FromBody]MessageModel model)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            Message message = new Message()
            {
                About = model.About,
                UserAccount_id = user.Id
            };
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/values/5
        [Authorize]
        [HttpPut("EditMessage/{messageId}")]
        public async Task<IActionResult> Put(int messageId, string value)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            if (_context.Messages.FirstOrDefault(t => t.Id == messageId).UserAccount_id == user.Id)
            {
                var message = _context.Messages.FirstOrDefault(t => t.Id == messageId);
                if (message != null)
                {
                    message.About = value;
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [Authorize]
        [HttpDelete("DeleteMessage/{Messageid}")]
        public async Task<IActionResult> Delete(int Messageid)
        {
            UserAccount user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            if (_context.Messages.FirstOrDefault(t => t.Id == Messageid).UserAccount_id == user.Id)
            {
                var message = _context.Messages.FirstOrDefault(t => t.Id == Messageid);
                if (message != null)
                {
                    _context.Messages.Remove(message);
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}

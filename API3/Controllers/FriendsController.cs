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
    public class FriendsController : ControllerBase
    {
        private readonly EFContext _context;

        public FriendsController(EFContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet("GetFriends")]
        public ContentResult Get()
        {
            List<Friends> friends = _context.Friends.ToList();
            string json = JsonConvert.SerializeObject(friends);
            return Content(json, "application/json");
        }



        // POST api/values
        [HttpPost]
        public void Post(Friends friends)
        {
            _context.Friends.Add(friends);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var friends = _context.Friends.FirstOrDefault(t => t.Id == id);
            if (friends != null)
            {
                _context.Remove(friends);
                _context.SaveChanges();
            }
        }
    }
}

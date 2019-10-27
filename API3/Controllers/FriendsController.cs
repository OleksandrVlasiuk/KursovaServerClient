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
        [HttpGet("GetFriend/{id}")]
        public ContentResult Get(string id)
        {
            List<UserAccount> friends = new List<UserAccount>();
           foreach (var item in _context.UserFriends.Where(t=>t.UserAccount_id==id))
            {
                friends.Add(item.FriendOf.UserAccountOf);
            }
            string json = JsonConvert.SerializeObject(friends);
            return Content(json, "application/json");
        }



        // POST api/values
        [HttpPost("PostFriend/{id}/{friendId}")]
        public void Post(string id,int friendId)
        {
            if (_context.UserFriends.FirstOrDefault(t => (t.UserAccount_id == id && t.FriendOf_id == friendId)) == null)
            {
                UserFriend ua = new UserFriend() {
                    UserAccount_id=id,
                    FriendOf_id=friendId
                };
                _context.UserFriends.Add(ua);
                _context.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("DeleteFriend/{id}/{friendId}")]
        public void Delete(string id,int friendId)
        {
            if (_context.UserFriends.FirstOrDefault(t => (t.UserAccount_id == id && t.FriendOf_id == friendId)) != null)
            {
                UserFriend ua = new UserFriend()
                {
                    UserAccount_id = id,
                    FriendOf_id = friendId
                };
                _context.UserFriends.Remove(ua);
                _context.SaveChanges();
            }
            }
    }
}

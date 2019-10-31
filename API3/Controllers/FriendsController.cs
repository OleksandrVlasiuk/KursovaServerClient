using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API3.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserAccount> _userManager;


        public FriendsController(EFContext context,UserManager<UserAccount> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET api/values
        [Authorize]
        [HttpGet("GetFriend")]
        public async Task<IActionResult> Get()
        {
            UserAccount login = await _userManager.FindByNameAsync(this.User.Identity.Name);
            List<UserAccount> friends = new List<UserAccount>();
           foreach (var item in _context.UserFriends.Where(t=>t.UserAccount_id==login.Id))
            {
                friends.Add(item.FriendOf.UserAccountOf);
            }
            string json = JsonConvert.SerializeObject(friends);
            return Content(json, "application/json");
        }



        // POST api/values
        [Authorize]
        [HttpPost("PostFriend/{friendId}")]
        public async Task<IActionResult> Post(int friendId)
        {
            UserAccount login = await _userManager.FindByNameAsync(this.User.Identity.Name);

            if (_context.UserFriends.FirstOrDefault(t => (t.UserAccount_id == login.Id && t.FriendOf_id == friendId)) == null)
            {

                UserFriend ua = new UserFriend() {
                    UserAccount_id=login.Id,
                    FriendOf_id=friendId
                };
                _context.UserFriends.Add(ua);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [Authorize]
        [HttpDelete("DeleteFriend/{friendId}")]
        public async Task<IActionResult> Delete(int friendId)
        {
            UserAccount login = await _userManager.FindByNameAsync(this.User.Identity.Name);
            if (_context.UserFriends.FirstOrDefault(t => (t.UserAccount_id == login.Id && t.FriendOf_id == friendId)) != null)
            {
                UserFriend ua = new UserFriend()
                {
                    UserAccount_id = login.Id,
                    FriendOf_id = friendId
                };
                _context.UserFriends.Remove(ua);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}

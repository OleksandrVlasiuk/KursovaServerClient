using API3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vcn.Entities;

namespace API3.Models
{
    public class UserAccountModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserFriend> UserFriends { get; set; }
    }
}

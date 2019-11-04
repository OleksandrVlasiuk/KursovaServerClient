using API3.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    [Table("tbl.UsersAccounts")]
    public class UserAccount:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Friend>Friends { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserFriend> UserFriends { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using vcn.Entities;

namespace API3.Entities
{
    [Table("tbl.UserFriends")]
    public class UserFriend
    {
        [Key,ForeignKey("UserAccountOf")]
        public string UserAccount_id { get; set; }
        public virtual UserAccount UserAccountOf { get; set; }
        [Key, ForeignKey("FriendOf")]
        public int FriendOf_id { get; set; }
        public virtual Friend FriendOf { get; set; }



    }
}

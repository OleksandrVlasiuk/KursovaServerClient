using API3.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    [Table("tbl.Friends")]
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccountOf")]
        public string UserAccount_id { get; set; }
        public virtual UserAccount UserAccountOf { get; set; }
        public virtual ICollection<UserFriend> UserFriends { get; set; }

    }
}
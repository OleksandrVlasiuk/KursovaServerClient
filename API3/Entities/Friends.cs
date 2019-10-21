using API3.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    public class Friends
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccountOf")]
        public int UserAccount_id { get; set; }
        public virtual UserAccount UserAccountOf { get; set; }

        [ForeignKey("FriendOf")]
        public int Friend_id { get; set; }
        public virtual UserAccount FriendOf { get; set; }

    }
}
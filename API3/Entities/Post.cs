using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    [Table("tbl.Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string File { get; set; }
        [Required]
        public int Likes { get; set; }
        [ForeignKey("UserAccountOf")]
        public int UserAccount_id { get; set; }
        [ForeignKey("FriendsOf")]
        public int Friends_id { get; set; }

        public virtual UserAccount UserAccountOf { get; set; }

        public virtual Friends FriendsOf { get; set; }
        public virtual ICollection<Comments>Comments{ get; set; }
    }
}
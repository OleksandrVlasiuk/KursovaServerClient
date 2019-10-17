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
        [ForeignKey("CommentOf")]
        public int CommentId { get; set; }
        public virtual Comments CommentOf { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
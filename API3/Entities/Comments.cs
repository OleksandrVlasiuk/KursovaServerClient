using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    [Table("tbl.Comments")]
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Info { get; set; }
        [ForeignKey("FriendsOf")]
        public int FriendsId { get; set; }
        public virtual Friends FriendsOf { get; set; }
        [ForeignKey("PostOf")]
        public int PostId { get; set; }
        public virtual Post PostOf { get; set; }
    }
}
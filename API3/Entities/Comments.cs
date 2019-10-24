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
        [ForeignKey("UserAccountOf")]
        public string UserAccountId { get; set; }
        public virtual UserAccount UserAccountOf { get; set; }
        [ForeignKey("PostOf")]
        public int PostId { get; set; }
        public virtual Post PostOf { get; set; }
    }
}
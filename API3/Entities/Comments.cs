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
        public virtual ICollection<Post> Posts { get; set; }
    }
}
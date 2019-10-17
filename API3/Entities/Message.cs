using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using vcn.Entities;

namespace API3.Entities
{
    [Table("tbl.Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int About { get; set; }
        public virtual ICollection<Friends> Friends { get; set; }

    }
}

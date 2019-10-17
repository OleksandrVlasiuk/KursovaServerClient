using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    public class Friends
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public UserAccount Person { get; set; }

    }
}
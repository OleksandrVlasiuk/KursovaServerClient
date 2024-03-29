﻿using System;
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
        public string About { get; set; }
        [ForeignKey("UserAccountOf")]
        public string UserAccount_id { get; set; }
        public virtual UserAccount UserAccountOf { get; set; }


    }
}

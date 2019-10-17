using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    [Table("tbl.UsersLogIn")]
    public class UserLoginning
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("UOf")]
        public virtual UserAccount UserAccountOf { get; set; }
        [ForeignKey("UserAccountOf")]
        public virtual UserAccount UserAccountOf { get; set; }
    }
}
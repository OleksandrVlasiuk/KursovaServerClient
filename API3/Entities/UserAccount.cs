using API3.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    [Table("tbl.UsersAccouns")]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<Friends>Friends { get; set; }
        public virtual UserLoginning UserLoginningOf { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
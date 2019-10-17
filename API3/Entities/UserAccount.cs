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
        [ForeignKey("FriendsOf")]
        public int Friends_id { get; set; }
        public virtual Friends FriendsOf { get; set; }
        public virtual UserLoginning UserLoginningOf { get; set; }
        [ForeignKey("PostOf")]
        public int Post_id { get; set; }
        public virtual Post PostOf { get; set; }
        [ForeignKey("MessageOf")]
        public int Message_id { get; set; }
        public virtual Message MessageOf { get; set; }
    }
}
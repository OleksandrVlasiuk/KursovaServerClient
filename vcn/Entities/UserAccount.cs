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
        public int Followers { get; set; }
        [Required]
        public int Followings { get; set; }
        [Required]
        public int CountOfPosts { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }

    }
}
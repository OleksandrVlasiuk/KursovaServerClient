using API3.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    public class Friends
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
        [Required, EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<UserAccount>UserAccounts { get; set; }
        [ForeignKey("MessageOf")]
        public int MessageId { get; set; }
        [ForeignKey("CommentOf")]
        public int CommentId { get; set; }
        public virtual Comments CommentOf { get; set; }
        public virtual Message MessageOf { get; set; }
    }
}
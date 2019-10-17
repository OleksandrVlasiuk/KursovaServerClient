using API3.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    public class EFContext:DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) :base(options)
        {

        }
        public virtual DbSet<Comments>Comments { get; set; }
        public virtual DbSet<Friends>Friends { get; set; }
        public virtual DbSet<Message>Messages { get; set; }
        public virtual DbSet<Post>Posts { get; set; }
        public virtual DbSet<UserAccount>UserAccounts { get; set; }
        public virtual DbSet<UserLoginning>UserLoginnings { get; set; } 
    }
}
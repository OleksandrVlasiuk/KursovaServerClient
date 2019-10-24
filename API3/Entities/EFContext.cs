using API3.Entities;
using LoginAPI.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vcn.Entities
{
    public class EFContext: IdentityDbContext<ApplicationLogin>
    {
        public EFContext(DbContextOptions<EFContext> options) :base(options)
        {

        }
        public virtual DbSet<Comments>Comments { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<UserFriend> UserFriends { get; set; }
        public virtual DbSet<Message>Messages { get; set; }
        public virtual DbSet<Post>Posts { get; set; }
        public virtual DbSet<UserAccount>UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriend>()
                .HasKey(o =>new { o.FriendOf_id, o.UserAccount_id });

            base.OnModelCreating(modelBuilder);
        }
    }
}
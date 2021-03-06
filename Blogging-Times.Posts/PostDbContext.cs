﻿using Blogging_Times.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Posts
{
    public class PostDbContext : DbContext
    {
        public PostDbContext() : base("DefaultConnection")
        {
            //
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostCategory> PostCategory { get; set; }
        public virtual DbSet<PostTag> PostTag { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove Pluralizing
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Remove Cascade Delete
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}

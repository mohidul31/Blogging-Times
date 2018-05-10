namespace Blogging_Times.Posts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4_Steps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Post", "CreatedBy", c => c.String());
            AddColumn("dbo.Post", "UpdatedBy", c => c.String());
            AddColumn("dbo.PostCategory", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.PostCategory", "CreatedBy", c => c.String());
            AddColumn("dbo.PostCategory", "UpdatedBy", c => c.String());
            AddColumn("dbo.PostTag", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.PostTag", "CreatedBy", c => c.String());
            AddColumn("dbo.PostTag", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostTag", "UpdatedBy");
            DropColumn("dbo.PostTag", "CreatedBy");
            DropColumn("dbo.PostTag", "UpdatedAt");
            DropColumn("dbo.PostCategory", "UpdatedBy");
            DropColumn("dbo.PostCategory", "CreatedBy");
            DropColumn("dbo.PostCategory", "UpdatedAt");
            DropColumn("dbo.Post", "UpdatedBy");
            DropColumn("dbo.Post", "CreatedBy");
            DropColumn("dbo.Post", "UpdatedAt");
        }
    }
}

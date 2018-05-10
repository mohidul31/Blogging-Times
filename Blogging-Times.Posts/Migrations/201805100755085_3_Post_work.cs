namespace Blogging_Times.Posts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3_Post_work : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Post", new[] { "PostCategory_ID" });
            AlterColumn("dbo.Post", "PostTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Post", "PostDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Post", "PostCategory_ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PostCategory", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.PostTag", "TagName", c => c.String(nullable: false));
            CreateIndex("dbo.Post", "PostCategory_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Post", new[] { "PostCategory_ID" });
            AlterColumn("dbo.PostTag", "TagName", c => c.String());
            AlterColumn("dbo.PostCategory", "CategoryName", c => c.String());
            AlterColumn("dbo.Post", "PostCategory_ID", c => c.Guid());
            AlterColumn("dbo.Post", "PostDescription", c => c.String());
            AlterColumn("dbo.Post", "PostTitle", c => c.String());
            CreateIndex("dbo.Post", "PostCategory_ID");
        }
    }
}

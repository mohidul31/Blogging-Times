namespace Blogging_Times.Posts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PostTitle = c.String(),
                        PostDescription = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        PostCategory_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PostCategory", t => t.PostCategory_ID)
                .Index(t => t.PostCategory_ID);
            
            CreateTable(
                "dbo.PostCategory",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CategoryName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostTag",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TagName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Post_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.Post_ID)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTag", "Post_ID", "dbo.Post");
            DropForeignKey("dbo.Post", "PostCategory_ID", "dbo.PostCategory");
            DropIndex("dbo.PostTag", new[] { "Post_ID" });
            DropIndex("dbo.Post", new[] { "PostCategory_ID" });
            DropTable("dbo.PostTag");
            DropTable("dbo.PostCategory");
            DropTable("dbo.Post");
        }
    }
}

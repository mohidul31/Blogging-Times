namespace Blogging_Times.Posts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyRelatrion_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostTag", "Post_ID", "dbo.Post");
            DropIndex("dbo.PostTag", new[] { "Post_ID" });
            CreateTable(
                "dbo.PostTagPost",
                c => new
                    {
                        PostTag_ID = c.Guid(nullable: false),
                        Post_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostTag_ID, t.Post_ID })
                .ForeignKey("dbo.PostTag", t => t.PostTag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.PostTag_ID)
                .Index(t => t.Post_ID);
            
            DropColumn("dbo.PostTag", "Post_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostTag", "Post_ID", c => c.Guid());
            DropForeignKey("dbo.PostTagPost", "Post_ID", "dbo.Post");
            DropForeignKey("dbo.PostTagPost", "PostTag_ID", "dbo.PostTag");
            DropIndex("dbo.PostTagPost", new[] { "Post_ID" });
            DropIndex("dbo.PostTagPost", new[] { "PostTag_ID" });
            DropTable("dbo.PostTagPost");
            CreateIndex("dbo.PostTag", "Post_ID");
            AddForeignKey("dbo.PostTag", "Post_ID", "dbo.Post", "ID");
        }
    }
}

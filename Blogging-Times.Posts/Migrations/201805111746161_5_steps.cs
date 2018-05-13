namespace Blogging_Times.Posts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5_steps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Username = c.String(maxLength: 450),
                        Password = c.String(nullable: false),
                        UserRoleEnum = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_ID)
                .Index(t => t.Username, unique: true)
                .Index(t => t.UpdatedBy_ID);
            
            AddColumn("dbo.Post", "UpdatedBy_ID", c => c.Guid());
            AddColumn("dbo.PostCategory", "UpdatedBy_ID", c => c.Guid());
            AddColumn("dbo.PostTag", "UpdatedBy_ID", c => c.Guid());
            CreateIndex("dbo.Post", "UpdatedBy_ID");
            CreateIndex("dbo.PostCategory", "UpdatedBy_ID");
            CreateIndex("dbo.PostTag", "UpdatedBy_ID");
            AddForeignKey("dbo.PostCategory", "UpdatedBy_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.PostTag", "UpdatedBy_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Post", "UpdatedBy_ID", "dbo.Users", "ID");
            DropColumn("dbo.Post", "UpdatedBy");
            DropColumn("dbo.PostCategory", "UpdatedBy");
            DropColumn("dbo.PostTag", "UpdatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostTag", "UpdatedBy", c => c.String());
            AddColumn("dbo.PostCategory", "UpdatedBy", c => c.String());
            AddColumn("dbo.Post", "UpdatedBy", c => c.String());
            DropForeignKey("dbo.Post", "UpdatedBy_ID", "dbo.Users");
            DropForeignKey("dbo.PostTag", "UpdatedBy_ID", "dbo.Users");
            DropForeignKey("dbo.PostCategory", "UpdatedBy_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "UpdatedBy_ID", "dbo.Users");
            DropIndex("dbo.PostTag", new[] { "UpdatedBy_ID" });
            DropIndex("dbo.Users", new[] { "UpdatedBy_ID" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.PostCategory", new[] { "UpdatedBy_ID" });
            DropIndex("dbo.Post", new[] { "UpdatedBy_ID" });
            DropColumn("dbo.PostTag", "UpdatedBy_ID");
            DropColumn("dbo.PostCategory", "UpdatedBy_ID");
            DropColumn("dbo.Post", "UpdatedBy_ID");
            DropTable("dbo.Users");
        }
    }
}

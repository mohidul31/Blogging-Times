namespace Blogging_Times.Posts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6_steps : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UpdatedBy_ID", "dbo.Users");
            DropIndex("dbo.Post", new[] { "UpdatedBy_ID" });
            DropIndex("dbo.PostCategory", new[] { "UpdatedBy_ID" });
            DropIndex("dbo.Users", new[] { "UpdatedBy_ID" });
            DropIndex("dbo.PostTag", new[] { "UpdatedBy_ID" });
            DropColumn("dbo.Post", "CreatedBy");
            DropColumn("dbo.PostCategory", "CreatedBy");
            DropColumn("dbo.PostTag", "CreatedBy");
            RenameColumn(table: "dbo.PostCategory", name: "UpdatedBy_ID", newName: "CreatedBy");
            RenameColumn(table: "dbo.PostTag", name: "UpdatedBy_ID", newName: "CreatedBy");
            RenameColumn(table: "dbo.Post", name: "UpdatedBy_ID", newName: "CreatedBy");
            AddColumn("dbo.Post", "UpdatedBy", c => c.Guid());
            AddColumn("dbo.PostCategory", "UpdatedBy", c => c.Guid());
            AddColumn("dbo.PostTag", "UpdatedBy", c => c.Guid());
            AlterColumn("dbo.Post", "CreatedBy", c => c.Guid());
            AlterColumn("dbo.PostCategory", "CreatedBy", c => c.Guid());
            AlterColumn("dbo.PostTag", "CreatedBy", c => c.Guid());
            CreateIndex("dbo.Post", "CreatedBy");
            CreateIndex("dbo.Post", "UpdatedBy");
            CreateIndex("dbo.PostCategory", "CreatedBy");
            CreateIndex("dbo.PostCategory", "UpdatedBy");
            CreateIndex("dbo.PostTag", "CreatedBy");
            CreateIndex("dbo.PostTag", "UpdatedBy");
            AddForeignKey("dbo.PostCategory", "UpdatedBy", "dbo.Users", "ID");
            AddForeignKey("dbo.PostTag", "UpdatedBy", "dbo.Users", "ID");
            AddForeignKey("dbo.Post", "UpdatedBy", "dbo.Users", "ID");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "UpdatedBy_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UpdatedBy_ID", c => c.Guid());
            AddColumn("dbo.Users", "CreatedBy", c => c.String());
            DropForeignKey("dbo.Post", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PostTag", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PostCategory", "UpdatedBy", "dbo.Users");
            DropIndex("dbo.PostTag", new[] { "UpdatedBy" });
            DropIndex("dbo.PostTag", new[] { "CreatedBy" });
            DropIndex("dbo.PostCategory", new[] { "UpdatedBy" });
            DropIndex("dbo.PostCategory", new[] { "CreatedBy" });
            DropIndex("dbo.Post", new[] { "UpdatedBy" });
            DropIndex("dbo.Post", new[] { "CreatedBy" });
            AlterColumn("dbo.PostTag", "CreatedBy", c => c.String());
            AlterColumn("dbo.PostCategory", "CreatedBy", c => c.String());
            AlterColumn("dbo.Post", "CreatedBy", c => c.String());
            DropColumn("dbo.PostTag", "UpdatedBy");
            DropColumn("dbo.PostCategory", "UpdatedBy");
            DropColumn("dbo.Post", "UpdatedBy");
            RenameColumn(table: "dbo.Post", name: "CreatedBy", newName: "UpdatedBy_ID");
            RenameColumn(table: "dbo.PostTag", name: "CreatedBy", newName: "UpdatedBy_ID");
            RenameColumn(table: "dbo.PostCategory", name: "CreatedBy", newName: "UpdatedBy_ID");
            AddColumn("dbo.PostTag", "CreatedBy", c => c.String());
            AddColumn("dbo.PostCategory", "CreatedBy", c => c.String());
            AddColumn("dbo.Post", "CreatedBy", c => c.String());
            CreateIndex("dbo.PostTag", "UpdatedBy_ID");
            CreateIndex("dbo.Users", "UpdatedBy_ID");
            CreateIndex("dbo.PostCategory", "UpdatedBy_ID");
            CreateIndex("dbo.Post", "UpdatedBy_ID");
            AddForeignKey("dbo.Users", "UpdatedBy_ID", "dbo.Users", "ID");
        }
    }
}

namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Forei : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "ItemID", "dbo.Items");
            DropIndex("dbo.Reviews", new[] { "ItemID" });
            AlterColumn("dbo.Reviews", "ItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "ItemID");
            AddForeignKey("dbo.Reviews", "ItemID", "dbo.Items", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ItemID", "dbo.Items");
            DropIndex("dbo.Reviews", new[] { "ItemID" });
            AlterColumn("dbo.Reviews", "ItemID", c => c.Int());
            CreateIndex("dbo.Reviews", "ItemID");
            AddForeignKey("dbo.Reviews", "ItemID", "dbo.Items", "Id");
        }
    }
}

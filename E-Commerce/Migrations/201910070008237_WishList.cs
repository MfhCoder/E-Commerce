namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WishLists", "ItemId", "dbo.Items");
            DropIndex("dbo.WishLists", new[] { "ItemId" });
            DropIndex("dbo.WishLists", new[] { "UserId" });
            DropTable("dbo.WishLists");
        }
    }
}

namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        ItemID = c.Int(),
                        Name = c.String(),
                        Email = c.String(),
                        Rate = c.Int(),
                        Review1 = c.String(),
                        DateTime = c.DateTime(),
                        isDelete = c.Boolean(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Items", t => t.ItemID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ItemID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "ItemID", "dbo.Items");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "ItemID" });
            DropTable("dbo.Reviews");
        }
    }
}

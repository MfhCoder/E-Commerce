namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "AddedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "UserId");
            AddForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "UserId" });
            DropColumn("dbo.Items", "UserId");
            DropColumn("dbo.Items", "Price");
            DropColumn("dbo.Items", "AddedDate");
        }
    }
}

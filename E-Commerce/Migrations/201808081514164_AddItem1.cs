namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItem1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Title", c => c.String());
            AddColumn("dbo.Items", "Description", c => c.String());
            AddColumn("dbo.Items", "ItemImage", c => c.String());
            DropColumn("dbo.Items", "JobTitle");
            DropColumn("dbo.Items", "JobContent");
            DropColumn("dbo.Items", "JobImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "JobImage", c => c.String());
            AddColumn("dbo.Items", "JobContent", c => c.String());
            AddColumn("dbo.Items", "JobTitle", c => c.String());
            DropColumn("dbo.Items", "ItemImage");
            DropColumn("dbo.Items", "Description");
            DropColumn("dbo.Items", "Title");
        }
    }
}

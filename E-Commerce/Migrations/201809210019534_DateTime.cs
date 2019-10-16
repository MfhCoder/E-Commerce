namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "AddedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "AddedDate", c => c.DateTime(nullable: false));
        }
    }
}

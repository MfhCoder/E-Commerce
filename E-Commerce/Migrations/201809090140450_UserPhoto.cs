namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UserPhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "UserPhoto");
        }
    }
}

namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropColumn("dbo.Reviews", "UserID");
            RenameColumn(table: "dbo.Reviews", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Reviews", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "UserID" });
            AlterColumn("dbo.Reviews", "UserID", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "UserID", newName: "User_Id");
            AddColumn("dbo.Reviews", "UserID", c => c.Int());
            CreateIndex("dbo.Reviews", "User_Id");
        }
    }
}

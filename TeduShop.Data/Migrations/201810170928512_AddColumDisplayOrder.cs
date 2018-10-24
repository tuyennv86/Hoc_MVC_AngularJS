namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumDisplayOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DisplayOrder", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DisplayOrder");
        }
    }
}

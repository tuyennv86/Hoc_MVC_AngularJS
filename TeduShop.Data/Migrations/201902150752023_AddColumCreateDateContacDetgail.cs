namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumCreateDateContacDetgail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContacDetails", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContacDetails", "CreatedDate");
        }
    }
}

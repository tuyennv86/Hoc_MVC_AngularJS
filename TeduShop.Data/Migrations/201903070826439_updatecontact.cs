namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecontact : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContacDetails", "LatMap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ContacDetails", "LngMap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContacDetails", "LngMap", c => c.Double(nullable: false));
            AlterColumn("dbo.ContacDetails", "LatMap", c => c.Double(nullable: false));
        }
    }
}

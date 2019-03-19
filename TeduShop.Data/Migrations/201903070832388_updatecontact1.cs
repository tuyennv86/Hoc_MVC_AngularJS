namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecontact1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContacDetails", "LatMap", c => c.String());
            AlterColumn("dbo.ContacDetails", "LngMap", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContacDetails", "LngMap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ContacDetails", "LatMap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

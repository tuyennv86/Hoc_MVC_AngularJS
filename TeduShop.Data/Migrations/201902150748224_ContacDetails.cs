namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContacDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContacDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 256),
                        Website = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Order = c.Int(nullable: false),
                        LatMap = c.Double(nullable: false),
                        LngMap = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContacDetails");
        }
    }
}

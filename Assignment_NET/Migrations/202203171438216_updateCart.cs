namespace Assignment_NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Images = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingCartModels");
        }
    }
}

namespace Assignment_NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "TenSanPham", c => c.String());
            AddColumn("dbo.OrderDetails", "Images", c => c.String());
            DropColumn("dbo.OrderDetails", "OrderId");
            DropColumn("dbo.OrderDetails", "Total");
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderNumber = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            AddColumn("dbo.OrderDetails", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Images");
            DropColumn("dbo.OrderDetails", "TenSanPham");
        }
    }
}

namespace hospital_project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookingtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        BookingName = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId);
            
            CreateTable(
                "dbo.Fees",
                c => new
                    {
                        FeeId = c.Int(nullable: false, identity: true),
                        FeeName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fees");
            DropTable("dbo.Bookings");
        }
    }
}

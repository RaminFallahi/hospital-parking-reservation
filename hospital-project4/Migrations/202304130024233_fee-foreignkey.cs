namespace hospital_project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feeforeignkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "FeeId", c => c.Int(nullable: false));
            AddColumn("dbo.Fees", "LotId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "BookingName", c => c.String());
            CreateIndex("dbo.Bookings", "FeeId");
            CreateIndex("dbo.Fees", "LotId");
            AddForeignKey("dbo.Fees", "LotId", "dbo.Lots", "LotId", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "FeeId", "dbo.Fees", "FeeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "FeeId", "dbo.Fees");
            DropForeignKey("dbo.Fees", "LotId", "dbo.Lots");
            DropIndex("dbo.Fees", new[] { "LotId" });
            DropIndex("dbo.Bookings", new[] { "FeeId" });
            AlterColumn("dbo.Bookings", "BookingName", c => c.Int(nullable: false));
            DropColumn("dbo.Fees", "LotId");
            DropColumn("dbo.Bookings", "FeeId");
        }
    }
}

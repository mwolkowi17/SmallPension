namespace Pensjonat2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationsID = c.Int(nullable: false, identity: true),
                        Room_Nr = c.Int(nullable: false),
                        Reservation_Owner = c.String(),
                        IsParkingNeed = c.Boolean(nullable: false),
                        IsEngilshBreakfest = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationsID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reservations");
        }
    }
}

namespace Pensjonat_ado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationsID = c.Int(nullable: false, identity: true),
                        IsParkingNeed = c.Boolean(nullable: false),
                        Reservation_Owner_GuestID = c.Int(),
                        Reserved_Room_RoomID = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationsID)
                .ForeignKey("dbo.Guests", t => t.Reservation_Owner_GuestID)
                .ForeignKey("dbo.Rooms", t => t.Reserved_Room_RoomID)
                .Index(t => t.Reservation_Owner_GuestID)
                .Index(t => t.Reserved_Room_RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Reserved_Room_RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "Reservation_Owner_GuestID", "dbo.Guests");
            DropIndex("dbo.Reservations", new[] { "Reserved_Room_RoomID" });
            DropIndex("dbo.Reservations", new[] { "Reservation_Owner_GuestID" });
            DropTable("dbo.Reservations");
        }
    }
}

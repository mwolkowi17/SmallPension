namespace Pensjonat_ado.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Nationality = c.String(),
                        SuperCardOwner = c.Boolean(nullable: false),
                        CreditCardNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuestID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Ifoccupied = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
            DropTable("dbo.Guests");
        }
    }
}

namespace Pensjonat2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "NrofRoom", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "NrofRoom");
        }
    }
}

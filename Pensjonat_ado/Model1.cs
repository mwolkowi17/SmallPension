namespace Pensjonat_ado
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    class Model1 : DbContext
    {
        public Model1()
            : base("name=PensjonatData")
        {
            
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public  DbSet<Room> Rooms { get; set; }
        public  DbSet<Guest> Guests { get; set; }
        public DbSet<Reservations> ReservationsB { get; set; }
    }
}

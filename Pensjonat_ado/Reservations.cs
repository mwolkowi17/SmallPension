using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat_ado
{
    class Reservations
    {
        public int ReservationsID { get; set; }
        public Room Reserved_Room { get; set; }
        public Guest Reservation_Owner { get; set; }
        // public DateTime ArrivalTime;
        // public DateTime DepartureTime;
        public bool IsParkingNeed { get; set; }
        //public Breakfest Reservation_Breakfest = new Breakfest();
    }
}

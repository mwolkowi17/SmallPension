using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat_ado
{
    enum RoomType
    {
        single, doublebed, threebed
    }
    class Room
    {
        public int RoomID { get; set; }
        public int Number { get; set; }
        public bool Ifoccupied { get; set; }
        public RoomType Type { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Number + " - " + Type + " - " + Price;
        }
    }
}

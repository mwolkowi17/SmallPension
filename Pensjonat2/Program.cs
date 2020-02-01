using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat2
{

    public enum RoomType
    {
        single, doublebed, threebed
    }
   public class Room
    {
        public int RoomID { get; set; }
        public int Number { get; set; }
        public bool Ifoccupied { get; set; }
        public RoomType Type { get; set; }
        public double Price { get; set; }

        public Room()
        {

        }

        public Room(int number, RoomType type, double price)
        {
            Number = number;
            Type = type;
            Price = price;
        }

        public override string ToString()
        {
            return Number + " - " + Type + " - " + Price;
        }

    }

    public class Hotel
    {
        public static List<Room> rooms = new List<Room>();
        public static List<Guest> guests = new List<Guest>();

        public void Add_Room(int number, RoomType type, double price)
        {
            Room room = new Room(number, type, price);
            rooms.Add(room);
        }
    }

    public class Guest
    {
        public int GuestID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nationality { get; set; }
        public bool SuperCardOwner { get; set; }
        public int CreditCardNumber { get; set; }
        public int NrofRoom { get; set; }

        public Queue<Room> history_of_resrvations = new Queue<Room>();

        public Guest(string name, string surname, string nationality, bool supercardowner, int creditcardnumber)
        {
            Name = name;
            Surname = surname;
            Nationality = nationality;
            SuperCardOwner = supercardowner;
            CreditCardNumber = creditcardnumber;
        }
        public Guest()
        {

        }

        public override string ToString()
        {
            return Name + " " + Surname;

        }

    }

    class Menager
    {
        public string Name;
        public string Surname;
        public int Pasword;
        public bool Islogged;

    }

    public class Reservations
    {
        public int ReservationsID { get; set; }
        public int Room_Nr { get; set; }
        public string Reservation_Owner { get; set; }
        // public DateTime ArrivalTime; tu warto ustalić to jako int a dopiero w konstruktorze dodać Sytem.DateTime.Month etc
        // public DateTime DepartureTime;
        public bool IsParkingNeed { get; set; }
        public bool IsEngilshBreakfest { get; set; }





    }

   

    class Breakfest
    {
        public bool IsContinental;
        public bool IsEnglish;

    }
    class Program
    {
        static void Main(string[] args)
        {
            Hotel hotel = new Hotel();
            Room roomA = new Room(1, RoomType.doublebed, 100);
            Room roomB = new Room(2, RoomType.doublebed, 100);
            Room roomC = new Room(3, RoomType.doublebed, 100);
            Room roomD = new Room(4, RoomType.single, 80);

            using (Model1 context = new Model1())
            {
                Hotel.rooms = context.Rooms.ToList();
                //Hotel.rooms.Add(roomA);
                //Hotel.rooms.Add(roomB);
                // Hotel.rooms.Add(roomD);
                //Hotel.rooms.Add(roomC);
                //context.Rooms.Add(roomA);
                //context.Rooms.Add(roomB);
                //context.Rooms.Add(roomC);
                //context.Rooms.Add(roomD);
                //context.SaveChanges();

                foreach (var n in Hotel.rooms)
                {
                    Console.WriteLine(n);
                }

                Console.WriteLine("===================================");

                ReservationBook book = new ReservationBook();

               // book.Make_Reservation("Iwona", "Wołkowicz", "Polish", false, 1244, RoomType.doublebed);
               book.Make_Reservation("Maria", "Gruber", "Polish", false, 3113431, RoomType.doublebed);

                //book.Display_Details_of_Reservation("Pawlak");
                //book.Add_Breakfest(1, true);
              //book.Cancel_Reservation("Maria", "Gruber");

                Console.ReadKey();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat2
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

    class Hotel
    {
        public static List<Room> rooms = new List<Room>();
        public static List<Guest> guests = new List<Guest>();

        public void Add_Room(int number, RoomType type, double price)
        {
            Room room = new Room(number, type, price);
            rooms.Add(room);
        }
    }

    class Guest
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

    class Reservations
    {

        public Room Reserved_Room;
        public Guest Reservation_Owner;
        // public DateTime ArrivalTime; tu warto ustalić to jako int a dopiero w konstruktorze dodać Sytem.DateTime.Month etc
        // public DateTime DepartureTime;
        public bool IsParkingNeed;
        public Breakfest Reservation_Breakfest = new Breakfest();





    }

    class ReservationBook
    {
        public List<Reservations> reservation_list = new List<Reservations>();

        public void Make_Reservation(string name, string surname, string nationality, bool supercardowner, int creditcardnumber, RoomType type)
        {
            using (Model1 context = new Model1())
            {
                Guest guest = new Guest(name, surname, nationality, supercardowner, creditcardnumber);
                Hotel.rooms = context.Rooms.ToList();
                List<Room> robocza = (from Room item in Hotel.rooms
                                      where item.Type == type
                                      select item).ToList();
                Room roboczyPokoj = (from Room item in robocza
                                     where item.Ifoccupied == false
                                     select item).First();
                Reservations reservation = new Reservations();

                reservation.Reservation_Owner = guest;
                reservation.Reserved_Room = roboczyPokoj;
                roboczyPokoj.Ifoccupied = true;


                context.Guests.Add(guest);
                Hotel.guests = context.Guests.ToList();
                guest.NrofRoom = roboczyPokoj.Number;
                context.SaveChanges();

                reservation_list.Add(reservation);
                guest.history_of_resrvations.Enqueue(roboczyPokoj);
                Console.WriteLine("Rezerwacja zrobiona dla: " + guest.Name + " " + guest.Surname);
            }
        }

        public void Cancel_Reservation(string name, string surname)
        {
            using (Model1 context = new Model1())
            {
               /* Reservations robocza = (from Reservations item in reservation_list
                                        where item.Reservation_Owner.Surname == surname && item.Reservation_Owner.Name == name
                                        select item).First();
                reservation_list.Remove(robocza);
                robocza.Reserved_Room.Ifoccupied = false;*/
               
                Guest roboczygosc = (from Guest item in context.Guests.ToList()
                                           where item.Surname == surname && item.Name==name
                                           select item).First();
                

                //Hotel.rooms = context.Rooms.ToList();
                Room roboczy = (from Room item in context.Rooms.ToList()
                                where item.Number == roboczygosc.NrofRoom
                                select item).First();
                roboczy.Ifoccupied = false;
                roboczygosc.NrofRoom = 0;

                context.SaveChanges();
                Console.WriteLine("Reservation of " + name + " " + surname + " is canceled.");
            }
        }

        public void Add_Breakfest(int number, bool isenglish)
        {
            Reservations robocza = new Reservations();
            robocza = (from Reservations item in reservation_list
                       where item.Reserved_Room.Number == number
                       select item).First();

            if (isenglish == true)
            {
                robocza.Reservation_Breakfest.IsEnglish = true;
            }
            else
            {
                robocza.Reservation_Breakfest.IsContinental = true;
            }
        }

        public void Add_Discount(int number)
        {
            Reservations robocza = (from Reservations item in reservation_list
                                    where item.Reserved_Room.Number == number
                                    select item).First();

            robocza.Reserved_Room.Price = robocza.Reserved_Room.Price - 0.2 * robocza.Reserved_Room.Price;
        }

        public void Show_Reservations()
        {
            foreach (var n in reservation_list)
            {
                Console.WriteLine(n.Reservation_Owner.Surname + " " + n.Reservation_Owner.Name + "Nr pokoju: " + n.Reserved_Room.Number + " " + n.Reserved_Room.Type);
            }
        }

        public void Display_Details_of_Reservation(string surname)
        {
            var roboczy = (from Reservations item in reservation_list
                           where item.Reservation_Owner.Surname == surname
                           select item).First();
            Console.WriteLine(roboczy.Reservation_Owner.Surname + " " + roboczy.Reservation_Owner.Name + " Numer pokoju:  " + roboczy.Reserved_Room.Number);
        }
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

                //book.Make_Reservation("Łucja", "Wołkowicz", "Polish", false, 1244, RoomType.doublebed);
                //book.Make_Reservation("Milosz", "Kowalski", "Polish", false, 31131, RoomType.single);
                book.Show_Reservations();
                //book.Display_Details_of_Reservation("Kowalski");
                //book.Add_Breakfest(1, true);
                book.Cancel_Reservation("Milosz", "Kowalski");

                Console.ReadKey();
            }
        }
    }
}

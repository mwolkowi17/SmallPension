using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat
{
    enum RoomType
    {
        single, doublebed, threebed
    }
    class Room
    {
        public int Number;
        public bool Ifoccupied;
        public RoomType Type;
        public double Price;

        public Room()
        {

        }

        public Room (int number, RoomType type,double price)
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
        public static List<Room> rooms= new List<Room>();

        public void Add_Room(int number, RoomType type, double price)
        {
            Room room = new Room(number, type, price);
            rooms.Add(room);
        }
    }

    class Guest
    {
        public string Name;
        public string Surname;
        public string Nationality;
        public bool SuperCardOwner;
        public int CreditCardNumber;

        public Queue<Room> history_of_resrvations= new Queue<Room>();

        public Guest (string name, string surname, string nationality, bool supercardowner, int creditcardnumber)
        {
            Name = name;
            Surname = surname;
            Nationality = nationality;
            SuperCardOwner = supercardowner;
            CreditCardNumber = creditcardnumber;
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
        public Breakfest Reservation_Breakfest=new Breakfest();

       

        

    }

    class ReservationBook
    {
        public List<Reservations> reservation_list= new List<Reservations>();

        public void Make_Reservation(string name, string surname, string nationality, bool supercardowner, int creditcardnumber, RoomType type)
        {
            Guest guest = new Guest(name, surname, nationality, supercardowner, creditcardnumber);

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

            reservation_list.Add(reservation);
            guest.history_of_resrvations.Enqueue(roboczyPokoj);
            Console.WriteLine(  "Rezerwacja zrobiona dla: "+guest.Name+" "+guest.Surname);
        }

        public void Cancel_Reservation(string name, string surname)
        {
            Reservations robocza = (from Reservations item in reservation_list
                                    where item.Reservation_Owner.Surname == surname && item.Reservation_Owner.Name == name
                                    select item).First();
            reservation_list.Remove(robocza);
            robocza.Reserved_Room.Ifoccupied = false;
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

        public void Add_Discount (int number)
        {
            Reservations robocza = (from Reservations item in reservation_list
                                    where item.Reserved_Room.Number == number
                                    select item).First();

            robocza.Reserved_Room.Price = robocza.Reserved_Room.Price - 0.2 * robocza.Reserved_Room.Price;
        }

        public void Show_Reservations()
        {
        foreach(var n in reservation_list)
            {
                Console.WriteLine(n.Reservation_Owner.Surname +" "+ n.Reservation_Owner.Name+ "Nr pokoju: "+ n.Reserved_Room.Number + " "+n.Reserved_Room.Type);
            }
        }

        public void Display_Details_of_Reservation(string surname)
        {
            var roboczy = (from Reservations item in reservation_list
                           where item.Reservation_Owner.Surname == surname
                           select item).First();
            Console.WriteLine(roboczy.Reservation_Owner.Surname +" " +roboczy.Reservation_Owner.Name+" Numer pokoju:  "+roboczy.Reserved_Room.Number);
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
            Hotel.rooms.Add(roomA);
            Hotel.rooms.Add(roomB);
            Hotel.rooms.Add(roomD);
            Hotel.rooms.Add(roomC);

            foreach (var n in Hotel.rooms)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("===================================");

            ReservationBook book = new ReservationBook();

            book.Make_Reservation("Marcin", "Wołkowicz", "Polish", false, 1244, RoomType.doublebed);
            book.Make_Reservation("Łukasz", "Kowalski", "Polish", false, 31131, RoomType.doublebed);
            book.Show_Reservations();
            book.Display_Details_of_Reservation("Kowalski");
            book.Add_Breakfest(1, true);
            
            
            Console.ReadKey();

        }
    }
}

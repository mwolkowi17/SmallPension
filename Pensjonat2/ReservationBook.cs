using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat2
{
    public class ReservationBook
    {


        public List<Reservations> reservation_list = new List<Reservations>();

        public List<Room> DisplayRooms()
        {
            using (Model1 context = new Model1())
            {
                List<Room> rooms_hotel = context.Rooms.ToList();
                return rooms_hotel;

            }

        }

        public List<Guest> DisplayGuests()
        {
            using(Model1 context=new Model1())
            {
                List<Guest> guests_hotel = context.Guests.ToList();
                return guests_hotel;
            }
        }
       
       

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


                //reservation.Reservation_Owner = guest;
                //reservation.Reserved_Room = roboczyPokoj;
                roboczyPokoj.Ifoccupied = true;

                var check = (from Guest item in context.Guests.ToList()
                             select item.Name).ToList();
               
                if (check.Contains(guest.Name))
                {
                    Guest oldroboczy = (from Guest item in context.Guests.ToList()
                                        where item.Name == guest.Name
                                        select item).First();
                    oldroboczy.NrofRoom = roboczyPokoj.Number;
                    
                }
                else
                {
                    context.Guests.Add(guest);
                    context.SaveChanges();
                    guest.NrofRoom = roboczyPokoj.Number;

                }
               

                //context.Guests.Add(guest);
                //Hotel.guests = context.Guests.ToList();
               // guest.NrofRoom = roboczyPokoj.Number;
                Reservations res_list = new Reservations();
                res_list.Reservation_Owner = (name + " " + surname);
                res_list.Room_Nr = roboczyPokoj.Number;
                context.Reservations_List.Add(res_list);
                context.SaveChanges();

                reservation_list.Add(res_list);
                guest.history_of_resrvations.Enqueue(roboczyPokoj);
                Console.WriteLine("Rezerwacja zrobiona dla: " + guest.Name + " " + guest.Surname);
            }
        }

        public Guest Make_Reservation_Obj(string name, string surname, string nationality, bool supercardowner, int creditcardnumber, RoomType type)
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


                //reservation.Reservation_Owner = guest;
                //reservation.Reserved_Room = roboczyPokoj;
                roboczyPokoj.Ifoccupied = true;

                var check = (from Guest item in context.Guests.ToList()
                             select item.Name).ToList();

                if (check.Contains(guest.Name))
                {
                    Guest oldroboczy = (from Guest item in context.Guests.ToList()
                                        where item.Name == guest.Name
                                        select item).First();
                    oldroboczy.NrofRoom = roboczyPokoj.Number;

                }
                else
                {
                    context.Guests.Add(guest);
                    context.SaveChanges();
                    guest.NrofRoom = roboczyPokoj.Number;

                }


                //context.Guests.Add(guest);
                //Hotel.guests = context.Guests.ToList();
                // guest.NrofRoom = roboczyPokoj.Number;
                Reservations res_list = new Reservations();
                res_list.Reservation_Owner = (name + " " + surname);
                res_list.Room_Nr = roboczyPokoj.Number;
                context.Reservations_List.Add(res_list);
                context.SaveChanges();

                reservation_list.Add(res_list);
                guest.history_of_resrvations.Enqueue(roboczyPokoj);
                return guest;
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
                                     where item.Surname == surname && item.Name == name
                                     select item).First();


                //Hotel.rooms = context.Rooms.ToList();
                Room roboczy = (from Room item in context.Rooms.ToList()
                                where item.Number == roboczygosc.NrofRoom
                                select item).First();
                roboczy.Ifoccupied = false;
                roboczygosc.NrofRoom = 0;

                Reservations sing_res = (from Reservations item in context.Reservations_List
                                         where item.Room_Nr == roboczy.Number
                                         select item).First();
                context.Reservations_List.Remove(sing_res);

                context.SaveChanges();
                Console.WriteLine("Reservation of " + name + " " + surname + " is canceled.");
            }
        }
        public Guest CancelReservationObj(int numer_klienta)
        {
            using (Model1 context = new Model1())
            {
                /* Reservations robocza = (from Reservations item in reservation_list
                                         where item.Reservation_Owner.Surname == surname && item.Reservation_Owner.Name == name
                                         select item).First();
                 reservation_list.Remove(robocza);
                 robocza.Reserved_Room.Ifoccupied = false;*/

                Guest roboczygosc = (from Guest item in context.Guests.ToList()
                                     where item.GuestID==numer_klienta
                                     select item).First();


                //Hotel.rooms = context.Rooms.ToList();
                Room roboczy = (from Room item in context.Rooms.ToList()
                                where item.Number == roboczygosc.NrofRoom
                                select item).First();
                roboczy.Ifoccupied = false;
                roboczygosc.NrofRoom = 0;

                Reservations sing_res = (from Reservations item in context.Reservations_List
                                         where item.Room_Nr == roboczy.Number
                                         select item).First();
                context.Reservations_List.Remove(sing_res);

                context.SaveChanges();

                return roboczygosc;
            }
        }

        public Guest DeleteGuest(int id)
        {
            using (Model1 context=new Model1())
            {
                Guest roboczy = (from Guest item in context.Guests.ToList()
                                 where item.GuestID == id
                                 select item).First();
                context.Guests.Remove(roboczy);
                context.SaveChanges();
                    
                return roboczy;
            }

        }
        /* public void Add_Breakfest(int number, bool isenglish)
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
         }*/

        /* public void Add_Discount(int number)
         {
             Reservations robocza = (from Reservations item in reservation_list
                                     where item.Reserved_Room.Number == number
                                     select item).First();

             robocza.Reserved_Room.Price = robocza.Reserved_Room.Price - 0.2 * robocza.Reserved_Room.Price;
         }*/

        /* public void Show_Reservations()
         {
             foreach (var n in reservation_list)
             {
                 Console.WriteLine(n.Reservation_Owner.Surname + " " + n.Reservation_Owner.Name + "Nr pokoju: " + n.Reserved_Room.Number + " " + n.Reserved_Room.Type);
             }
         }*/

        /* public void Display_Details_of_Reservation(string surname)
         {
             var roboczy = (from Reservations item in reservation_list
                            where item.Reservation_Owner.Surname == surname
                            select item).First();
             Console.WriteLine(roboczy.Reservation_Owner.Surname + " " + roboczy.Reservation_Owner.Name + " Numer pokoju:  " + roboczy.Reserved_Room.Number);
         }*/
    }
}

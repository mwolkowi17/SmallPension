using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pensjonat_ado
{
    class Guest
    {
        public int GuestID {get;set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nationality { get; set; }
        public bool SuperCardOwner { get; set; }
        public int CreditCardNumber { get; set; }

        public Queue<Room> history_of_resrvations = new Queue<Room>();

        public Guest(string name, string surname, string nationality, bool supercardowner, int creditcardnumber)
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pensjonat2;

namespace SmallHotelWPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> lista = new List<string>();

        public MainWindow()
        {
            lista.Add("Guests");
            lista.Add("Rooms");
            InitializeComponent();
            Lista.ItemsSource = lista;
            // DisplayList();
            if ((string)Lista.SelectedItem == lista[1])
            {
                ReservationBook resbook = new ReservationBook();
                Tresc.ItemsSource = resbook.DisplayRooms();
            }
           
        }

        public string DisplayList()
        {
            //if (Lista.SelectedItem == lista[0]) {
                ReservationBook resbook = new ReservationBook();
                Tresc.ItemsSource = resbook.DisplayRooms();

            //}
            return "a";
        }

        private void Lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)Lista.SelectedItem == "Rooms")
            {
                ReservationBook resbook = new ReservationBook();
                Tresc.ItemsSource = resbook.DisplayRooms();
            }

            if ((string)Lista.SelectedItem == "Guests")
            {
                ReservationBook resbook = new ReservationBook();
                Tresc.ItemsSource = resbook.DisplayGuests();

            }
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            
            ReservationBook resbook = new ReservationBook();
            string roboczyId = DoUsuniecia.Text;
            int roboczyIdInt = Convert.ToInt32(roboczyId);
            resbook.DeleteGuest(roboczyIdInt);
            DoUsuniecia.Text = "Usunięto!";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Lista.SelectedItem == "Rooms")
            {
                ReservationBook resbook = new ReservationBook();
                Tresc.ItemsSource = resbook.DisplayRooms();
            }

            if ((string)Lista.SelectedItem == "Guests")
            {
                ReservationBook resbook = new ReservationBook();
                Tresc.ItemsSource = resbook.DisplayGuests();

            }
        }

        private void CancBut_Click(object sender, RoutedEventArgs e)
        {
            ReservationBook resbook = new ReservationBook();
            string roboczyCanc = CanText.Text;
            int roboczyCancInt = Convert.ToInt32(roboczyCanc);
            resbook.CancelReservationObj(roboczyCancInt);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReservationBook resbook = new ReservationBook();
            string roboczyName = nameText.Text;
            string roboczySurname = surnameText.Text;
            string roboczyNation = nationalityText.Text;
            bool roboczySuperCard = false;
            string roboczyCard = cardnrText.Text;
            int roboczyCardInt = Convert.ToInt32(roboczyCard);
            RoomType roboczyroomtype = RoomType.doublebed;
            int roboczyroomtypeInt = Convert.ToInt32(roboczyroomtype);

            resbook.Make_Reservation_Obj(roboczyName, roboczySurname, roboczyNation, roboczySuperCard, roboczyCardInt, roboczyroomtype);


        }
    }
}

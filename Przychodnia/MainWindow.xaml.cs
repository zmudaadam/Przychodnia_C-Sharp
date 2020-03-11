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
using System.Data.SqlClient;
using System.Data;

namespace Przychodnia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Wylacz_program_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyłączyć program?", "Zamknij", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Add_pacjent_Click(object sender, RoutedEventArgs e)
        {
            Panel_dolny.Content = new Page_dodawanie_pacjenta();
        }

        private void Zobacz_pacjent_Click(object sender, RoutedEventArgs e)
        {
            Panel_dolny.Content = new Page_pacjenci();
        }

        private void Zobacz_kalendarz_Click(object sender, RoutedEventArgs e)
        {
            Panel_dolny.Content = new Page_kalendarz();
        }

        private void Zobacz_notatki_Click(object sender, RoutedEventArgs e)
        {
            Panel_dolny.Content = new Page_notatki();
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen wylogowanie = new LoginScreen();
            wylogowanie.Show();
            this.Close();
            MessageBox.Show("Wylogowano pomyślnie", "Wylogowano"); 
        }
    }
}
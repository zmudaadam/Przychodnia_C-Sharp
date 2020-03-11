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
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Data;


namespace Przychodnia
{
    public partial class Page_kalendarz : Page
    {
        public Page_kalendarz()
        {
            InitializeComponent();
            binddatagrid_kalendarz();
            zegarek();
        }

        public LoginScreen LoginScreen
        {
            get => default(LoginScreen);
            set
            {
            }
        }

        string dbPacjentConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZmudaCorp\Desktop\Przychodnia\Przychodnia\DB\BazaDanych.mdf;Integrated Security=True;Connect Timeout=30";

        private void binddatagrid_kalendarz()
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            try
            {
                sqlconPacjent.Open();
                SqlCommand cmd_pacjent = new SqlCommand();
                cmd_pacjent.CommandText = "select Imie,Nazwisko,Data,Godzina from [tbl_rejestracja]";
                cmd_pacjent.Connection = sqlconPacjent;
                SqlDataAdapter da_pacjenci = new SqlDataAdapter(cmd_pacjent);
                DataTable dt_pacjenci = new DataTable("Pacjenci");
                da_pacjenci.Fill(dt_pacjenci);

                datagrid_kalendarz.ItemsSource = dt_pacjenci.DefaultView;
                sqlconPacjent.Close();
            }
            catch (Exception ex_dg_kalendarz)
            {
                MessageBox.Show(ex_dg_kalendarz.Message);
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void zegarek()
        {
            DispatcherTimer zegar = new DispatcherTimer();
            zegar.Interval = TimeSpan.FromSeconds(1);
            zegar.Tick += tykanie;
            zegar.Start();
        }

        private void tykanie(object sender, EventArgs e)
        {
            txt_zegar.Text = DateTime.Now.ToString();
        }
    }
}
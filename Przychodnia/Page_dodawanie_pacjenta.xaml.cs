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
    public partial class Page_dodawanie_pacjenta : Page
    {
        public Page_dodawanie_pacjenta()
        {
            InitializeComponent();
            binddatagrid_pacjenci();
        }
        string dbPacjentConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZmudaCorp\Desktop\Przychodnia\Przychodnia\DB\BazaDanych.mdf;Integrated Security=True;Connect Timeout=30";

        private void binddatagrid_pacjenci()
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            try
            {
                sqlconPacjent.Open();
                SqlCommand cmd_pacjent = new SqlCommand();
                cmd_pacjent.CommandText = "select ID,Imie,Nazwisko from [tbl_rejestracja]";
                cmd_pacjent.Connection = sqlconPacjent;
                SqlDataAdapter da_pacjenci = new SqlDataAdapter(cmd_pacjent);
                DataTable dt_pacjenci = new DataTable("Pacjenci");
                da_pacjenci.Fill(dt_pacjenci);

                datagrid_pacjenci.ItemsSource = dt_pacjenci.DefaultView;
                sqlconPacjent.Close();
            }
            catch (Exception ex_dg_rejestracja)
            {
                MessageBox.Show(ex_dg_rejestracja.Message);
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void Btn_rejestracja_zapisz_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            SqlCommand cmd_pacjent = new SqlCommand();
            if (sqlconPacjent.State != ConnectionState.Open)
                sqlconPacjent.Open();
            cmd_pacjent.Connection = sqlconPacjent;

            try
            {
                 if (txt_rejestracja_ID.IsEnabled == true)
                 {
                    cmd_pacjent.CommandText = @"INSERT INTO tbl_rejestracja (ID, Imie, Nazwisko) VALUES (@ID, @Imie, @Nazwisko)";
                    cmd_pacjent.Parameters.AddWithValue("@ID", txt_rejestracja_ID.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Imie", txt_rejestracja_imie.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Nazwisko", txt_rejestracja_nazwisko.Text);

                    int a = cmd_pacjent.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Zapis udany pomyślnie", "Zapis");
                        binddatagrid_pacjenci();
                        Wyczysc_Pola();
                    }
                 }
                else
                {
                    cmd_pacjent.CommandText = "update tbl_rejestracja set Imie='" + txt_rejestracja_imie.Text + "',Nazwisko= '" + txt_rejestracja_nazwisko.Text + "' where ID=" + txt_rejestracja_ID.Text;
                    cmd_pacjent.ExecuteNonQuery();
                    binddatagrid_pacjenci();
                    MessageBox.Show("Zaaktualizowano dane","Aktualizacja danych");
                    Wyczysc_Pola();
                }
            }
            catch (Exception ex_zapisz_rejestracja)
            {
                MessageBox.Show("Wpisz poprawne dane/uzupełnij wszystkie pola." +ex_zapisz_rejestracja.Message, "Uwaga!");
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void Btn_rejestracja_usun_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć pozycję?", "Usuwanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (datagrid_pacjenci.SelectedItems.Count > 0)
                {
                    SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
                    DataRowView row = (DataRowView)datagrid_pacjenci.SelectedItems[0];
                    SqlCommand cmd_pacjent = new SqlCommand();
                    if (sqlconPacjent.State != ConnectionState.Open)
                        sqlconPacjent.Open();
                    cmd_pacjent.Connection = sqlconPacjent;
                    cmd_pacjent.CommandText = "delete from [tbl_rejestracja] where ID=" + row["ID"].ToString();
                    cmd_pacjent.ExecuteNonQuery();

                    binddatagrid_pacjenci();
                    MessageBox.Show("Usunięto pomyślnie", "Usuwanie");
                    Wyczysc_Pola();
                }
                else
                {
                    MessageBox.Show("Wybierz jakaś pozycję z listy", "Uwaga!");
                }
            }
        }

        private void Btn_rejestracja_anuluj_Click(object sender, RoutedEventArgs e)
        {
            Wyczysc_Pola();
        }

        private void Wyczysc_Pola()
        {
            txt_rejestracja_ID.Text = "";
            txt_rejestracja_imie.Text = "";
            txt_rejestracja_nazwisko.Text = "";

            txt_rejestracja_ID.IsEnabled = true;

            btn_rejestracja_zapisz.Content = "Zapisz";
        }   
    }
}
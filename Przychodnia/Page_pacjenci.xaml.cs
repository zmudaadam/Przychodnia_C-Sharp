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
    public partial class Page_pacjenci : Page
    {
        public Page_pacjenci()
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
                cmd_pacjent.CommandText = "select * from [tbl_rejestracja]";
                cmd_pacjent.Connection = sqlconPacjent;
                SqlDataAdapter da_pacjenci = new SqlDataAdapter(cmd_pacjent);
                DataTable dt_pacjenci = new DataTable("Pacjenci");
                da_pacjenci.Fill(dt_pacjenci);

                datagrid_pacjenci.ItemsSource = dt_pacjenci.DefaultView;
                sqlconPacjent.Close();
            }
            catch (Exception ex_dg_pacjenci) 
            {
                MessageBox.Show(ex_dg_pacjenci.Message);
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void Btn_pacjent_zapisz_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            SqlCommand cmd_pacjent = new SqlCommand();
            if (sqlconPacjent.State != ConnectionState.Open)
                sqlconPacjent.Open();
            cmd_pacjent.Connection = sqlconPacjent;

            try
            {
                if (txt_pacjent_ID.IsEnabled == true)
                {
                    cmd_pacjent.CommandText = @"INSERT INTO tbl_rejestracja (ID, Imie, Nazwisko, Adres, Telefon, Wiek, Plec, Data, Godzina, Specjalizacja, Prowadzacy, Opis, Uwagi) VALUES (@ID, @Imie, @Nazwisko, @Adres, @Telefon, @Wiek, @Plec, @Data, @Godzina, @Specjalizacja, @Prowadzacy, @Opis, @Uwagi)";
                    cmd_pacjent.Parameters.AddWithValue("@ID", txt_pacjent_ID.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Imie", txt_pacjent_imie.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Nazwisko", txt_pacjent_nazwisko.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Adres", txt_pacjent_adres.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Telefon", txt_pacjent_telefon.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Wiek", txt_pacjent_wiek.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Plec", combo_pacjent_plec.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Data", txt_pacjent_data.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Godzina", txt_pacjent_godzina.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Specjalizacja", combo_pacjent_l_f.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Prowadzacy", combo_pacjent_i_l_f.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Opis", txt_pacjent_opis.Text);
                    cmd_pacjent.Parameters.AddWithValue("@Uwagi", txt_pacjent_uwagi.Text);

                    int a = cmd_pacjent.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Zapis udany pomyślnie", "Zapis");
                        binddatagrid_pacjenci();
                        Wyczysc_Pola_Pacjenci();
                    }
                }
                else
                {
                    cmd_pacjent.CommandText = "update tbl_rejestracja set Imie='" + txt_pacjent_imie.Text + "',Nazwisko= '" + txt_pacjent_nazwisko.Text + "',Adres='" + txt_pacjent_adres.Text + "',Telefon= '" + txt_pacjent_telefon.Text + "',Wiek= '" + txt_pacjent_wiek.Text + "',Plec= '" + combo_pacjent_plec.Text + "',Data= '" + txt_pacjent_data.Text + "', Godzina= '" + txt_pacjent_godzina.Text + "',Specjalizacja= '" + combo_pacjent_l_f.Text + "',Prowadzacy= '" + combo_pacjent_i_l_f.Text + "',Opis= '" + txt_pacjent_opis.Text + "', Uwagi= '" + txt_pacjent_uwagi.Text + "' where ID=" + txt_pacjent_ID.Text;
                    cmd_pacjent.ExecuteNonQuery();
                    binddatagrid_pacjenci();
                    MessageBox.Show("Zaaktualizowano dane", "Aktualizacja danych");
                    Wyczysc_Pola_Pacjenci();
                }
            }

            catch (Exception ex_zapisz_pacjent)
            {
                MessageBox.Show("Wpisz poprawne dane/uzupełnij wszystkie pola.", "Uwaga!");
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void Btn_pacjent_usun_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            try
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć pozycję?", "Usuwanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (datagrid_pacjenci.SelectedItems.Count > 0)
                    {
                        DataRowView row = (DataRowView)datagrid_pacjenci.SelectedItems[0];
                        SqlCommand cmd_pacjent = new SqlCommand();
                        if (sqlconPacjent.State != ConnectionState.Open)
                            sqlconPacjent.Open();
                        cmd_pacjent.Connection = sqlconPacjent;
                        cmd_pacjent.CommandText = "delete from [tbl_rejestracja] where ID=" + row["ID"].ToString();
                        cmd_pacjent.ExecuteNonQuery();

                        binddatagrid_pacjenci();
                        MessageBox.Show("Usunięto pomyślnie", "Usuwanie");
                        Wyczysc_Pola_Pacjenci();
                    }
                    else
                    {
                        MessageBox.Show("Wybierz jakaś pozycję z listy", "Uwaga!");
                    }
                }
            }
            catch (Exception ex_pacjent_usun)
            {
                MessageBox.Show("Wybierz jakaś pozycję z listy", "Uwaga!");
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void Btn_pacjent_edytuj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);

            if (datagrid_pacjenci.SelectedItems.Count > 0)
            {
                sqlconPacjent.Open();
                DataRowView row = (DataRowView)datagrid_pacjenci.SelectedItems[0];

                txt_pacjent_ID.Text = row["ID"].ToString();
                txt_pacjent_imie.Text = row["Imie"].ToString();
                txt_pacjent_nazwisko.Text = row["Nazwisko"].ToString();
                txt_pacjent_adres.Text = row["Adres"].ToString();
                txt_pacjent_telefon.Text = row["Telefon"].ToString();
                txt_pacjent_wiek.Text = row["Wiek"].ToString();
                combo_pacjent_plec.Text = row["Plec"].ToString();
                txt_pacjent_data.Text = row["Data"].ToString();
                txt_pacjent_godzina.Text = row["Godzina"].ToString();
                combo_pacjent_l_f.Text = row["Specjalizacja"].ToString();
                combo_pacjent_i_l_f.Text = row["Prowadzacy"].ToString();
                txt_pacjent_opis.Text = row["Opis"].ToString();
                txt_pacjent_uwagi.Text = row["Uwagi"].ToString();

                txt_pacjent_ID.IsEnabled = false;

                btn_pacjent_zapisz.Content = "Zapisz zmiany";
            }
            else
            {
                MessageBox.Show("Wybierz pozycję by edytować", "Uwaga!");

            }
        }

        private void Btn_pacjent_szukaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            try
            {
                if (combo_pacjent_szukaj.Text == "ID")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where ID LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Imie")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Imie LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Nazwisko")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Nazwisko LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Adres")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Adres LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Telefon")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Telefon LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Wiek")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Wiek LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Plec")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Plec LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Data")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Data LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Godzina")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Godzina LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Specjalizacja")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Specjalizacja LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Prowadzacy")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Prowadzacy LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Opis")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Opis LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_pacjent_szukaj.Text == "Uwagi")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_rejestracja] where Uwagi LIKE '" + txt_pacjent_szukaj.Text + "%'", sqlconPacjent);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_pacjenci.ItemsSource = data_szukaj.DefaultView;
                }
            }
            catch (Exception ex_pacjent_szukaj)
            {
                MessageBox.Show("Błąd szukania", "Błąd");
            }
        }

        private void Btn_pacjent_odswiez_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPacjent = new SqlConnection(dbPacjentConnectionString);
            try
            {
                sqlconPacjent.Open();
                SqlCommand cmd_pacjent = new SqlCommand();
                cmd_pacjent.CommandText = "select * from [tbl_rejestracja]";
                cmd_pacjent.Connection = sqlconPacjent;
                SqlDataAdapter da_pacjenci = new SqlDataAdapter(cmd_pacjent);
                DataTable dt_pacjenci = new DataTable("Pacjenci");
                da_pacjenci.Fill(dt_pacjenci);

                datagrid_pacjenci.ItemsSource = dt_pacjenci.DefaultView;
                sqlconPacjent.Close();
            }
            catch (Exception ex_dg_pacjenci)
            {
                MessageBox.Show(ex_dg_pacjenci.Message);
            }
            finally
            {
                sqlconPacjent.Close();
            }
        }

        private void Btn_pacjent_anuluj_Click(object sender, RoutedEventArgs e)
        {
            Wyczysc_Pola_Pacjenci();
        }

        private void Wyczysc_Pola_Pacjenci()
        {
            txt_pacjent_ID.Text = "";
            txt_pacjent_imie.Text = "";
            txt_pacjent_nazwisko.Text = "";
            txt_pacjent_adres.Text = "";
            txt_pacjent_telefon.Text = "";
            txt_pacjent_wiek.Text = "";
            combo_pacjent_plec.Text = "";
            txt_pacjent_data.Text = "";
            txt_pacjent_godzina.Text = "";
            combo_pacjent_l_f.Text = "";
            combo_pacjent_i_l_f.Text = "";
            txt_pacjent_opis.Text = "";
            txt_pacjent_uwagi.Text = "";

            txt_pacjent_ID.IsEnabled = true;

            btn_pacjent_zapisz.Content = "Zapisz";
        }
    }
}
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
    public partial class Page_notatki : Page
    {
        public Page_notatki()
        {
            InitializeComponent();
            binddatagrid_notatka();
        }

        public Page_dodawanie_pacjenta Page_dodawanie_pacjenta
        {
            get => default(Page_dodawanie_pacjenta);
            set
            {
            }
        }

        string dbNotatkaConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZmudaCorp\Desktop\Przychodnia\Przychodnia\DB\BazaDanych.mdf;Integrated Security=True;Connect Timeout=30";

        private void binddatagrid_notatka() 
        {
            SqlConnection sqlconNotatka = new SqlConnection(dbNotatkaConnectionString);
            try
            {
                sqlconNotatka.Open();
                SqlCommand cmd_notatka = new SqlCommand();
                cmd_notatka.CommandText = "select * from [tbl_notatka]";
                cmd_notatka.Connection = sqlconNotatka;
                SqlDataAdapter da_notatka = new SqlDataAdapter(cmd_notatka);
                DataTable dt_notatka = new DataTable("Notatka");
                da_notatka.Fill(dt_notatka);

                datagrid_notatka.ItemsSource = dt_notatka.DefaultView;
                sqlconNotatka.Close();
            }
            catch (Exception ex_dg_notatka)
            {
                MessageBox.Show(ex_dg_notatka.Message);
            }
            finally
            {
                sqlconNotatka.Close();
            }

        }

        private void Btn_notatka_zapisz_Click(object sender, RoutedEventArgs e)
        
        {
            SqlConnection sqlconNotatka = new SqlConnection(dbNotatkaConnectionString);
            SqlCommand cmd_notatka = new SqlCommand();
            if (sqlconNotatka.State != ConnectionState.Open)
                sqlconNotatka.Open();
            cmd_notatka.Connection = sqlconNotatka;

            try
            {
                if (txt_notatka_ID.IsEnabled == true)
                {
                    cmd_notatka.CommandText = @"INSERT INTO tbl_notatka (ID, Temat, Notatka) VALUES (@ID, @Temat,@Notatka)";
                    cmd_notatka.Parameters.AddWithValue("@ID", txt_notatka_ID.Text);
                    cmd_notatka.Parameters.AddWithValue("@Temat", txt_notatka_temat.Text);
                    cmd_notatka.Parameters.AddWithValue("@Notatka", txt_notatka_opis.Text);


                    int a = cmd_notatka.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Zapis udany pomyślnie", "Zapis");
                        binddatagrid_notatka();
                        Wyczysc_Pola_Notatka();
                    }
                }
                else
                {
                    cmd_notatka.CommandText = "update tbl_notatka set Temat='" + txt_notatka_temat.Text + "',Notatka= '" + txt_notatka_opis.Text + "' where ID=" + txt_notatka_ID.Text;
                    cmd_notatka.ExecuteNonQuery();
                    binddatagrid_notatka();
                    MessageBox.Show("Zaaktualizowano dane", "Aktualizacja danych");
                    Wyczysc_Pola_Notatka();
                }
            }

            catch (Exception ex_notatka_zapis)
            {
                MessageBox.Show("Wpisz poprawne dane/uzupełnij wszystkie pola.", "Uwaga!");
            }
            finally
            {
                sqlconNotatka.Close();
            }
        }

        private void Btn_notatka_usun_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconNotatka = new SqlConnection(dbNotatkaConnectionString);

            try
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć pozycję?", "Usuwanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (datagrid_notatka.SelectedItems.Count > 0)
                    {
                        DataRowView row = (DataRowView)datagrid_notatka.SelectedItems[0];
                        SqlCommand cmd_notatka = new SqlCommand();
                        if (sqlconNotatka.State != ConnectionState.Open)
                            sqlconNotatka.Open();
                        cmd_notatka.Connection = sqlconNotatka;
                        cmd_notatka.CommandText = "delete from [tbl_notatka] where ID=" + row["ID"].ToString();
                        cmd_notatka.ExecuteNonQuery();

                        binddatagrid_notatka();
                        MessageBox.Show("Usunięto pomyślnie", "Usuwanie");
                        Wyczysc_Pola_Notatka();
                    }
                    else
                    {
                        MessageBox.Show("Wybierz jakaś pozycję z listy", "Uwaga!");
                    }
                }
            }
            catch (Exception ex_notatka_usun)
            {
                MessageBox.Show("Wybierz jakaś pozycję z listy", "Uwaga!");
            }
            finally
            {
                sqlconNotatka.Close();
            }
        }

        private void Btn_notatka_edytuj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconNotatka = new SqlConnection(dbNotatkaConnectionString);

            if (datagrid_notatka.SelectedItems.Count > 0)
            {
                sqlconNotatka.Open();
                DataRowView row = (DataRowView)datagrid_notatka.SelectedItems[0];

                txt_notatka_ID.Text = row["ID"].ToString();
                txt_notatka_temat.Text = row["Temat"].ToString();
                txt_notatka_opis.Text = row["Notatka"].ToString();

                txt_notatka_ID.IsEnabled = false;

                btn_notatka_zapisz.Content = "Zapisz zmiany";
            }
            else
            {
                MessageBox.Show("Wybierz pozycję by edytować", "Uwaga!");

            }
        }
        private void Btn_notatka_szukaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconNotatka = new SqlConnection(dbNotatkaConnectionString);
            try
            {
                if (combo_notatka_szukaj.Text == "ID")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_notatka] where ID LIKE '" + txt_notatka_szukaj.Text + "%'", sqlconNotatka);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_notatka.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_notatka_szukaj.Text == "Temat")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_notatka] where Temat LIKE '" + txt_notatka_szukaj.Text + "%'", sqlconNotatka);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_notatka.ItemsSource = data_szukaj.DefaultView;
                }
                else if (combo_notatka_szukaj.Text == "Notatka")
                {
                    SqlDataAdapter szukaj = new SqlDataAdapter("select * from [tbl_notatka] where Notatka LIKE '" + txt_notatka_szukaj.Text + "%'", sqlconNotatka);
                    DataTable data_szukaj = new DataTable();
                    szukaj.Fill(data_szukaj);
                    datagrid_notatka.ItemsSource = data_szukaj.DefaultView;
                }
            }
            catch (Exception ex_notatka_szukaj)
            {
                MessageBox.Show("Błąd szukania", "Błąd");
            }
        }
        private void Btn_notatka_anuluj_Click(object sender, RoutedEventArgs e)
        {
            Wyczysc_Pola_Notatka();
        }

        private void Wyczysc_Pola_Notatka()
        {
            txt_notatka_ID.Text = "";
            txt_notatka_temat.Text = "";
            txt_notatka_opis.Text = "";
            txt_notatka_ID.IsEnabled = true;

            btn_notatka_zapisz.Content = "Zapisz";
        }

        private void Btn_notatka_odswiez_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconNotatka = new SqlConnection(dbNotatkaConnectionString);
            try
            {
                sqlconNotatka.Open();
                SqlCommand cmd_notatka = new SqlCommand();
                cmd_notatka.CommandText = "select * from [tbl_notatka]";
                cmd_notatka.Connection = sqlconNotatka;
                SqlDataAdapter da_notatka = new SqlDataAdapter(cmd_notatka);
                DataTable dt_notatka = new DataTable("Notatka");
                da_notatka.Fill(dt_notatka);

                datagrid_notatka.ItemsSource = dt_notatka.DefaultView;
                sqlconNotatka.Close();
            }
            catch (Exception ex_dg_notatka)
            {
                MessageBox.Show(ex_dg_notatka.Message);
            }
            finally
            {
                sqlconNotatka.Close();
            }
        }
    }
}
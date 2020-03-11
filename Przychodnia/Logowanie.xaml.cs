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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Przychodnia
{

    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

 

        string dbLoginConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZmudaCorp\Desktop\Przychodnia\Przychodnia\DB\BazaDanych.mdf;Integrated Security=True;Connect Timeout=30";

        private void ButtonLogowanie_Click(object sender, RoutedEventArgs e)
        {   SqlConnection sqlconLogin = new SqlConnection(dbLoginConnectionString);
           
            try 
            {
                if (sqlconLogin.State == System.Data.ConnectionState.Closed);
                sqlconLogin.Open(); 
                string query = "SELECT COUNT(1) FROM tbl_Login WHERE username=@username AND password=@password";
                SqlCommand sqlCmdLogin = new SqlCommand(query, sqlconLogin);
                sqlCmdLogin.CommandType = System.Data.CommandType.Text;
                sqlCmdLogin.Parameters.AddWithValue("@username", txtLogin.Text);
                sqlCmdLogin.Parameters.AddWithValue("@password", txtHaslo.Password);
                int count = Convert.ToInt32(sqlCmdLogin.ExecuteScalar());

                    if (count == 1)
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();

                        this.Close(); 
                        MessageBox.Show("Zalogowano pomyślnie", "Logowanie");
                    }
                    else 
                    { 
                        MessageBox.Show("Login lub hasło jest nieprawidłowe","Błąd logowania");
                    }  
            }
            catch (Exception ex_logowania) 
            {
                MessageBox.Show(ex_logowania.Message);
            }
            finally
            {
                sqlconLogin.Close();
            }
        }

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyłączyć program?", "Zamknij", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }  
        }
    }
}

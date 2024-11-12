using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace TOSOT_Praktika
{
    public partial class PagePasswordUpdate : Page
    {
        Model1Container1 db;
        public PagePasswordUpdate()
        {
            InitializeComponent();
            db = new Model1Container1();
        }
        string dbConnectionString = @"Data Source=VETA-PC;Initial Catalog=TOSOT;Integrated Security=True";
        private void SaveNewPassword_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            conn.Open();
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(newPassword.Password))
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.Worker.Select(item => item.Login).Contains(Login.Text))
            {
                SqlCommand comm = new SqlCommand("Update dbo.Worker Set Password='" + newPassword.Password + "'  Where Login = '" + Login.Text + "'", conn);
                SqlDataAdapter adapt4 = new SqlDataAdapter(comm);
                DataTable tbl4 = new DataTable();
                adapt4.Fill(tbl4);
                comm.Parameters.AddWithValue("Password", newPassword.Password);
            }
            else
            {
                MessageBoxLoginNotExist mblne = new MessageBoxLoginNotExist();
                mblne.Show();
                return;
            }  
            MessageBoxChange mbch = new MessageBoxChange();
            mbch.Show();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageAuthorisation.xaml", UriKind.Relative));
        }
    }
}

       
    



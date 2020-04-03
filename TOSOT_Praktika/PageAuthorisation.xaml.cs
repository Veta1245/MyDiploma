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
    public partial class PageAuthorisation : Page
    {
        TOSOT db; 
        public PageAuthorisation()
        {
            InitializeComponent();
            db = new TOSOT();        
        }
        string dbConnectionString = @"Data Source=VETA-PC;Initial Catalog=TOSOT;Integrated Security=True";
        private void Authorisation_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnectionString);
            conn.Open();
            if (Login.Text == "" || Password.Password == "")
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.Worker.Select(item => item.Login + "" + item.Password).Contains(Login.Text + "" + Password.Password))
            {
                SqlCommand comm = new SqlCommand("Select FirstName From Worker Where Login='" + Login.Text + "'", conn);
                SqlDataAdapter adapt4 = new SqlDataAdapter(comm);
                DataTable tbl4 = new DataTable();
                adapt4.Fill(tbl4);
                FirstNameWelcome.Text = tbl4.Rows[0]["FirstName"].ToString();
                MainWindow mw = new MainWindow();
                mw.LabelText = FirstNameWelcome.Text;
                mw.Show();
                }
                else
                {
                    MessageBoxErrorLoginPassword mbelp = new MessageBoxErrorLoginPassword();
                    mbelp.Show();
                }
            }
        private void PasswordUpdate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagePasswordUpdate.xaml", UriKind.Relative));
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageRegistration.xaml", UriKind.Relative));
        }
    }
}


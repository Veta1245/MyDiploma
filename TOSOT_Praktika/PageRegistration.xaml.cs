using System;
using System.Collections.Generic;
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
    public partial class PageRegistration : Page
    {
       Model1Container1 db;
        public PageRegistration()
        {
            InitializeComponent();
            db = new Model1Container1();
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (Registration.IsInitialized)
            {
                FIO.Text = LastNameWorker.Text + " " + FirstNameWorker.Text + " " + MiddleNameWorker.Text;
            }
            if (string.IsNullOrWhiteSpace(LastNameWorker.Text) || string.IsNullOrWhiteSpace(FirstNameWorker.Text) || string.IsNullOrWhiteSpace(MiddleNameWorker.Text) || string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.Worker.Select(item => item.Login).Contains(Login.Text))
            {
                MessageBoxExistingLogin mbel = new MessageBoxExistingLogin();
                mbel.Show();
                return;
            }
            Worker Newworker = new Worker()
            {
                Login = Login.Text,
                Password = Password.Password,
                LastName = LastNameWorker.Text,
                FirstName = FirstNameWorker.Text,
                MiddleName = MiddleNameWorker.Text,                
            };
            db.Worker.Add(Newworker);
            db.SaveChanges();
            MessageBoxSuccessRegistration mbsr = new MessageBoxSuccessRegistration();
            mbsr.Show();  
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageAuthorisation.xaml", UriKind.Relative));
        }
    }
}

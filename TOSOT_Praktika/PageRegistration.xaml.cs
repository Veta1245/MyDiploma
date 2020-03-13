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

namespace TOSOT_Praktika
{
    /// <summary>
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        TOSOT db;
        public PageRegistration()
        {
            InitializeComponent();
            db = new TOSOT();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (Registration.IsInitialized)
            {
                FIO.Text = LastNameWorker.Text + " " + FirstNameWorker.Text + " " + MiddleNameWorker.Text;

            }
            if (LastNameWorker.Text == "" || FirstNameWorker.Text == "" || MiddleNameWorker.Text == "" || Login.Text == "" || Password.Password == "")
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
            MessageBox.Show("Вы успешно зарегистрировались!");
            
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageAuthorisation.xaml", UriKind.Relative));
        }
    }
}

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
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        TOSOT db;
        public MainPage()
        {
            InitializeComponent();
            db = new TOSOT();
        }
        private void insert_NewStudent(object sender, RoutedEventArgs e)
        {
            NewStudent ns = new NewStudent();
            ns.Show();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListStudent.ItemsSource = db.Student.ToList();
            ListStudent.ScrollIntoView(ListStudent.Items[ListStudent.Items.Count - 1]);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ListStudent.ItemsSource = db.Student.ToList().Where(x=>x.Firm.Name_Firm.StartsWith(EntryField.Text)||x.LastName.StartsWith(EntryField.Text)).ToList();

        }

        private void CertificateLog_Click(object sender, RoutedEventArgs e)
        {
            CertificateLog cl = new CertificateLog();
            cl.Show();

        }
    }
}

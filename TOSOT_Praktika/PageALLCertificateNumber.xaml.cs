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
    /// Логика взаимодействия для PageALLCertificateNumber.xaml
    /// </summary>
    public partial class PageALLCertificateNumber : Page
    {
        TOSOT db;
        public PageALLCertificateNumber()
        {
            InitializeComponent();
            db = new TOSOT();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListAllCertificateNumber.ItemsSource = db.RegistrationCertificates.ToList();
        }
        private void ot_Loaded(object sender, RoutedEventArgs e)
        {
            OT.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(ot.Header)));
        }

        private void height_Loaded(object sender, RoutedEventArgs e)
        {
            Height.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(height.Header)));
        }
    }
}

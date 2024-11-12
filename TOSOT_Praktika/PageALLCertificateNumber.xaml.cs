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
    public partial class PageALLCertificateNumber : Page
    {
        Model1Container1 db;
        public PageALLCertificateNumber()
        {
            InitializeComponent();
            db = new Model1Container1();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {        
            (ListAllCertificateNumber.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            ListAllCertificateNumber.ItemsSource = db.RegistrationCertificates.ToList();
        }
        private void ot_Loaded(object sender, RoutedEventArgs e)
        {
            (OT.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            OT.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(ot.Header)));
        }
        private void height_Loaded(object sender, RoutedEventArgs e)
        {
            (Height.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            Height.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(height.Header)));
        }
        private void ptm_Loaded(object sender, RoutedEventArgs e)
        {
            (PTM.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            PTM.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(ptm.Header)));
        }
        private void opp_Loaded(object sender, RoutedEventArgs e)
        {
            (OPP.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            OPP.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(opp.Header)));
        }
        private void ts_Loaded(object sender, RoutedEventArgs e)
        {
            (TS.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            TS.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(ts.Header)));
        }
        private void steril_Loaded(object sender, RoutedEventArgs e)
        {
            (Steril.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            Steril.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(steril.Header)));
        }
        private void ps_Loaded(object sender, RoutedEventArgs e)
        {
            (PS.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            PS.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(ps.Header)));
        }
        private void worker_Loaded(object sender, RoutedEventArgs e)
        {
            (Worker.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            Worker.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(worker.Header)));
        }
        private void rs_Loaded(object sender, RoutedEventArgs e)
        {
            (RS.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            RS.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(rs.Header)));
        }
        private void ecology_Loaded(object sender, RoutedEventArgs e)
        {
            (Ecology.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            Ecology.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(ecology.Header)));
        }
        private void es_Loaded(object sender, RoutedEventArgs e)
        {
            (ES.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            ES.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(es.Header)));
        }
        private void goAndchs_Loaded(object sender, RoutedEventArgs e)
        {
            (GOAndCHS.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            GOAndCHS.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(goAndchs.Header)));
        }
        private void profPa_Loaded(object sender, RoutedEventArgs e)
        {
            (ProfPa.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            ProfPa.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(profPa.Header)));
        }
        private void medical_Loaded(object sender, RoutedEventArgs e)
        {
            (Medical.Columns[1] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            Medical.ItemsSource = db.RegistrationCertificates.ToList().Where(x => x.Code.StartsWith(Convert.ToString(medical.Header)));
        }
    }
}

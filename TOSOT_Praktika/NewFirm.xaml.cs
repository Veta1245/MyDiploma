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

namespace TOSOT_Praktika
{
    public partial class NewFirm : Window
    {
        Model1Container1 db;
        public NewFirm()
        {
            InitializeComponent();
            db = new Model1Container1();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameFirm.Text = NewStudent.PassingText;
        }
        private void closeNewFirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void insert_new_firm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameFirm.Text))
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.Firm.Select(item => item.Name_Firm).Contains(NameFirm.Text))
            {
                MessageBoxBusy mbb = new MessageBoxBusy();
                mbb.Show();
                return;
            }
            Firm newfirm = new Firm()
            {
                Name_Firm = NameFirm.Text
            };
            db.Firm.Add(newfirm);
            db.SaveChanges();
            this.Close();             
            MessageBoxInsert mbi = new MessageBoxInsert();
            mbi.Show();       
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}

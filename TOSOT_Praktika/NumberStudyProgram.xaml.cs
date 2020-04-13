using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class NumberStudyProgram : Window
    {
        TOSOT db;
        public static string PassingText;
        public NumberStudyProgram()
        {
            InitializeComponent();
            db = new TOSOT();
        }
        private void closeNumderStudyProgram_Click(object sender, RoutedEventArgs e)
        {
            FormNumberSertificate fns = new FormNumberSertificate();
            fns.Show();
            Close();
        }
        private void choice_study_program1_Click(object sender, RoutedEventArgs e)
        {
            PassingText = reserve.Text;
            FormNumberSertificate fns = new FormNumberSertificate();
            fns.Show();
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new TOSOT();
            list.ItemsSource = db.LearningProgram.OrderBy(x => x.KeyOfProgram.Length).ThenBy(x => x.KeyOfProgram).ToList();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void InsertNewProgram_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanelNewProgram.Visibility = Visibility.Visible;
        }
        private void UpdateNewProgram_Click(object sender, RoutedEventArgs e)
        {
            if (KeyOfProgram.Text == "" || NameProgram.Text == "")
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.LearningProgram.Select(item => item.KeyOfProgram).Contains(KeyOfProgram.Text))
            {
                MessageBoxBusy mbb = new MessageBoxBusy();
                mbb.Show();
                return;
            }
            int num = (list.SelectedItem as LearningProgram).ID_LearningProgram;
            var uRow = db.LearningProgram.Where(w => w.ID_LearningProgram == num).FirstOrDefault();
            uRow.Name = NameProgram.Text;
            uRow.KeyOfProgram = Convert.ToString(KeyOfProgram.Text);
            db.SaveChanges();
            list.ItemsSource = db.LearningProgram.ToList();
        }
    }
}


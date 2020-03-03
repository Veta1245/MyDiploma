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
using System.Management;
using System.Data.SqlClient;
using TOSOT_Praktika.Properties;

namespace TOSOT_Praktika
{
    /// <summary>
    /// Логика взаимодействия для FormNumberSertificate.xaml
    /// </summary>
    public partial class FormNumberSertificate : Window
    {
        TOSOT db;
        string text = string.Empty;
        public static string PassingText;
        public FormNumberSertificate()
        {
            InitializeComponent();
            db = new TOSOT();
            fill_ComboBox();

            yearLearning.Text = Properties.Settings.Default.TextBoxYearLearning;
            numberGroup.Text = Properties.Settings.Default.TextBoxNumberGroup;
            studyProgram.Text = Properties.Settings.Default.TextBoxStudyProgram;
            dateFilling.SelectedDate = Properties.Settings.Default.DatePickerDateFilling;
            listworker.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxListWorker);


        }
        
        public List<Worker> worker { get; set; }
        private void fill_ComboBox()
        {
            TOSOT db = new TOSOT();
            var item = db.Worker.ToList();
            worker = item;
            DataContext = worker;

        }

        private void closeFormNemderSertificate_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TextBoxYearLearning = yearLearning.Text;
            Properties.Settings.Default.TextBoxNumberGroup = numberGroup.Text;
            Properties.Settings.Default.TextBoxStudyProgram = studyProgram.Text;
            Properties.Settings.Default.DatePickerDateFilling = dateFilling.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxListWorker = listworker.SelectedIndex;

            Properties.Settings.Default.Save();

            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 2;
            ns.Show();

            this.Close();
        }

        private void choice_study_program_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TextBoxYearLearning = yearLearning.Text;
            Properties.Settings.Default.TextBoxNumberGroup = numberGroup.Text;
            Properties.Settings.Default.TextBoxStudyProgram = studyProgram.Text;
            Properties.Settings.Default.DatePickerDateFilling = dateFilling.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxListWorker = listworker.SelectedIndex;

            Properties.Settings.Default.Save();

            NumberStudyProgram nsp = new NumberStudyProgram();
            nsp.Show();
            this.Close();
        }

        private void force_number_sertificate1_Click(object sender, RoutedEventArgs e)
        {
            if(RegistrNumber.Text==""|| studyProgram.Text==""|| dateFilling.SelectedDate == null || listworker.SelectedItem == null)
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }


            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 2;
            ns.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            studyProgram.Text = NumberStudyProgram.PassingText;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public void collectNumber_Click(object sender, RoutedEventArgs e)
        {
            if (yearLearning.Text == "" || numberGroup.Text == "" || studyProgram.Text == "")
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }


            int id = Properties.Settings.Default.ID1;
            string idProperty = String.Format("{0:d4}", id);
            string registText = string.Empty;
            registText = $"{studyProgram.Text}{yearLearning.Text}{numberGroup.Text}{idProperty}";
            RegistrNumber.Text = registText;
            Properties.Settings.Default.ID1++;
            Properties.Settings.Default.Save();
        }

        private void studyProgram_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

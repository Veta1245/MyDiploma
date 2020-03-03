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
using Microsoft.Win32;


namespace TOSOT_Praktika
{
    /// <summary>
    /// Логика взаимодействия для NewStudent.xaml
    /// </summary>

    public partial class NewStudent : Window
    {
        TOSOT db;
        public static string PassingText;
        public static string PassingText2;
        public NewStudent()
        {

            InitializeComponent();
            db = new TOSOT();
            fill_Combobox();

            lastName.Text = Properties.Settings.Default.TextBoxlastname;
            firstName.Text = Properties.Settings.Default.TextBoxfirstname;
            middleName.Text = Properties.Settings.Default.TextBoxmiddlename;
            Birthday.SelectedDate = Properties.Settings.Default.datepickerbirthday;
            position.Text = Properties.Settings.Default.TextBoxposition;
            numberdiploma.Text = Properties.Settings.Default.TextBoxnumberdiploma;
            listFirm.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxlistfirm);
            listeducation.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxlisteducation);
            beginLearning.SelectedDate = Properties.Settings.Default.DatePickerBeginLearning;
            endLearning.SelectedDate = Properties.Settings.Default.DatePickerEndLearning;
            numberSertificate.Text = Properties.Settings.Default.TextBoxNumberSertificate;
            listLearningProgramm.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxListLearningProgram);
        }
        public List<Firm> firm { get; set; }
        private void fill_Combobox()
        {
            TOSOT db = new TOSOT();
            var item = db.Firm.ToList();
            firm = item;
            DataContext = firm;

        }

        private void close_NewStudent(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TextBoxlastname = lastName.Text;
            Properties.Settings.Default.TextBoxfirstname = firstName.Text;
            Properties.Settings.Default.TextBoxmiddlename = middleName.Text;
            Properties.Settings.Default.datepickerbirthday = Birthday.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxlistfirm = listFirm.SelectedIndex;
            Properties.Settings.Default.ComboBoxlisteducation = listeducation.SelectedIndex;
            Properties.Settings.Default.TextBoxposition = position.Text;
            Properties.Settings.Default.TextBoxnumberdiploma = numberdiploma.Text;
            Properties.Settings.Default.DatePickerBeginLearning = beginLearning.SelectedDate.Value;
            Properties.Settings.Default.DatePickerEndLearning = endLearning.SelectedDate.Value;
            Properties.Settings.Default.TextBoxNumberSertificate = numberSertificate.Text;
            Properties.Settings.Default.ComboBoxListLearningProgram = listLearningProgramm.SelectedIndex;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void select_newfirm(object sender, RoutedEventArgs e)
        {
            PassingText = listFirm.Text;
            NewFirm nf = new NewFirm();

            Properties.Settings.Default.TextBoxlastname = lastName.Text;
            Properties.Settings.Default.TextBoxfirstname = firstName.Text;
            Properties.Settings.Default.TextBoxmiddlename = middleName.Text;
            Properties.Settings.Default.datepickerbirthday = Birthday.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxlistfirm = listFirm.SelectedIndex;
            Properties.Settings.Default.ComboBoxlisteducation = listeducation.SelectedIndex;
            Properties.Settings.Default.TextBoxposition = position.Text;
            Properties.Settings.Default.TextBoxnumberdiploma = numberdiploma.Text;
            Properties.Settings.Default.DatePickerBeginLearning = beginLearning.SelectedDate.Value;
            Properties.Settings.Default.DatePickerEndLearning = endLearning.SelectedDate.Value;
            Properties.Settings.Default.TextBoxNumberSertificate = numberSertificate.Text;
            Properties.Settings.Default.ComboBoxListLearningProgram = listLearningProgramm.SelectedIndex;

            Properties.Settings.Default.Save();
            
            nf.Show();
            this.Close();

        }

        private void force_number_sertificate_Click(object sender, RoutedEventArgs e)
        {
            FormNumberSertificate fns = new FormNumberSertificate();

            Properties.Settings.Default.TextBoxlastname = lastName.Text;
            Properties.Settings.Default.TextBoxfirstname = firstName.Text;
            Properties.Settings.Default.TextBoxmiddlename = middleName.Text;
            Properties.Settings.Default.datepickerbirthday = Birthday.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxlistfirm = listFirm.SelectedIndex;
            Properties.Settings.Default.ComboBoxlisteducation = listeducation.SelectedIndex;
            Properties.Settings.Default.TextBoxposition = position.Text;
            Properties.Settings.Default.TextBoxnumberdiploma = numberdiploma.Text;
            Properties.Settings.Default.DatePickerBeginLearning = beginLearning.SelectedDate.Value;
            Properties.Settings.Default.DatePickerEndLearning = endLearning.SelectedDate.Value;
            Properties.Settings.Default.TextBoxNumberSertificate = numberSertificate.Text;
            Properties.Settings.Default.ComboBoxListLearningProgram = listLearningProgramm.SelectedIndex;

            Properties.Settings.Default.Save();

            fns.Show();
            this.Close();
        }

        private void foto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";

            if (openDialog.ShowDialog() == true)
            {
                foto.Source = new BitmapImage(new Uri(openDialog.FileName));
                return;
            }
        }
        
        private void foto_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void insert_new_learning_program_Click(object sender, RoutedEventArgs e)
        {
            PassingText2 = listLearningProgramm.Text;
           
            NewLearningProgram nlp = new NewLearningProgram();

            Properties.Settings.Default.TextBoxlastname = lastName.Text;
            Properties.Settings.Default.TextBoxfirstname = firstName.Text;
            Properties.Settings.Default.TextBoxmiddlename = middleName.Text;
            Properties.Settings.Default.datepickerbirthday = Birthday.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxlistfirm = listFirm.SelectedIndex;
            Properties.Settings.Default.ComboBoxlisteducation = listeducation.SelectedIndex;
            Properties.Settings.Default.TextBoxposition = position.Text;
            Properties.Settings.Default.TextBoxnumberdiploma = numberdiploma.Text;
            Properties.Settings.Default.DatePickerBeginLearning = beginLearning.SelectedDate.Value;
            Properties.Settings.Default.DatePickerEndLearning = endLearning.SelectedDate.Value;
            Properties.Settings.Default.TextBoxNumberSertificate = numberSertificate.Text;
            Properties.Settings.Default.ComboBoxListLearningProgram = listLearningProgramm.SelectedIndex;

            Properties.Settings.Default.Save();
            nlp.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void insert_newStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lastName.Text == "" || firstName.Text == "" || middleName.Text == "" || Birthday.SelectedDate == null || listFirm.SelectedItem == null || position.Text == "" || listeducation.SelectedItem == null || numberdiploma.Text == "" || beginLearning.SelectedDate == null || endLearning.SelectedDate == null || numberSertificate.Text == "" || listLearningProgramm.SelectedItem == null)
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
           // if(db.Student.Select(item=>item.LastName +""+ item.FirstName +""+ item.MiddleName + "" + item.Birthday + "" + item. Firm + "" + item.Post + "" + item.Education + "" + item.Number_of_certificate + "" + item. + "" +))
        }
    }
}

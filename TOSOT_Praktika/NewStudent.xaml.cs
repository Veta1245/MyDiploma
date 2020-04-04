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
using System.IO;

namespace TOSOT_Praktika
{
    public partial class NewStudent : Window
    {
        TOSOT db;
        public static string PassingText;
        public static string PassingText2;
        public NewStudent()
        {
            InitializeComponent();
            db = new TOSOT();           
            listFirm.ItemsSource = db.Firm.ToList();
            listLearningProgramm.ItemsSource = db.TrainingProgram.ToList();
            fill_Combobox();
            fill_Combobox1();
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
            foto.Source = new BitmapImage(new Uri(Properties.Settings.Default.Photo));
        }
        private void fill_Combobox1()
        {
            TOSOT db = new TOSOT();
            var item = db.TrainingProgram.ToList();
            NameProgram = item;
            DataContext = NameProgram;
        }
        public List<Firm> firm { get; set; }
        public List<TrainingProgram> NameProgram { get; set; }
        public string FilePath { get; set; }
        private void fill_Combobox()
        {
            TOSOT db = new TOSOT();
            var item = db.Firm.ToList();
            firm = item;
            DataContext = firm;
        }
        public void close_NewStudent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void select_newfirm(object sender, RoutedEventArgs e)
        {
            PassingText = listFirm.Text;
            NewFirm nf = new NewFirm();
            nf.Show();
        }
        private void force_number_sertificate_Click(object sender, RoutedEventArgs e)
        {
            FormNumberSertificate fns = new FormNumberSertificate();
            fns.Show();
            this.Close();
        }
        public void foto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (openDialog.ShowDialog() == true)
            {
                FilePath = openDialog.FileName;
                foto.Source = new BitmapImage(new Uri(openDialog.FileName)); 
                return;
            }
            else
            {
                FilePath = String.Empty;
            }
        }
        private void insert_new_learning_program_Click(object sender, RoutedEventArgs e)
        {
            PassingText2 = listLearningProgramm.Text;          
            NewLearningProgram nlp = new NewLearningProgram();
            nlp.Show();
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
            Firm firm = (Firm)listFirm.SelectedItem;
            TrainingProgram trainingProg = (TrainingProgram)listLearningProgramm.SelectedItem;
            if (lastName.Text == "" || firstName.Text == "" || middleName.Text == "" || Birthday.SelectedDate == null || listFirm.SelectedItem == null || position.Text == "" || listeducation.SelectedItem == null || numberdiploma.Text == "" || beginLearning.SelectedDate == null || endLearning.SelectedDate == null || numberSertificate.Text == "" || listLearningProgramm.SelectedItem == null)
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            Student NewStudent = new Student()
            {
                LastName = lastName.Text,
                FirstName = firstName.Text,
                MiddleName = middleName.Text,
                Birthday = Convert.ToDateTime(Birthday.SelectedDate),
                Firm = firm,
                Post = position.Text,
                NimberDiploma = numberdiploma.Text,
                Education = listeducation.Text,
                BeginLearning = Convert.ToDateTime(beginLearning.SelectedDate),
                EndLearning = Convert.ToDateTime(endLearning.SelectedDate),
                Number_of_certificate = numberSertificate.Text,
                TrainingProgram = trainingProg,
                Student_photo= ConvertImageToByteClass.ImageToByte(FilePath)
        };
            db.Student.Add(NewStudent);
            db.SaveChanges();
            MessageBoxInsert mbi = new MessageBoxInsert();
            mbi.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            numberSertificate.Text = FormNumberSertificate.PassingText1;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
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
            db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            Save();
        }
        private void Save()
        {
            listFirm.ItemsSource = db.Firm.ToList();
            listLearningProgramm.ItemsSource = db.TrainingProgram.ToList();  
        }
        private void Fix_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Photo = FilePath;
            Properties.Settings.Default.Save();
        }
    }
}

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
        Model1Container1 db = new Model1Container1();
        public static string PassingText;
        public static string PassingText2;
        public NewStudent()
        {
            InitializeComponent();
            LoadComboBox();
            LoadProperties();
            ClearForm();
        }

        private void ClearForm()
        {
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            middleName.Text = string.Empty;
            Birthday1.SelectedDate = DateTime.Now;
            foto.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/nophoto.jpg"));
            listFirm.SelectedItem = null;   
            position.Text = string.Empty;
            listeducation.SelectedItem = null;
            numberdiploma.Text = string.Empty;  
            beginLearning.SelectedDate = DateTime.Now;
            endLearning.SelectedDate = DateTime.Now;
            listLearningProgramm.SelectedItem = null;
        }

        void LoadComboBox()
        {
            listLearningProgramm.ItemsSource = db.TrainingProgram.ToList();
            listFirm.ItemsSource = db.Firm.ToList();
        }

        void LoadProperties()
        {
            lastName.Text = Properties.Settings.Default.TextBoxlastname;
            firstName.Text = Properties.Settings.Default.TextBoxfirstname;
            middleName.Text = Properties.Settings.Default.TextBoxmiddlename;
            Birthday1.SelectedDate = Properties.Settings.Default.datepickerbirthday;
            position.Text = Properties.Settings.Default.TextBoxposition;
            numberdiploma.Text = Properties.Settings.Default.TextBoxnumberdiploma;
            listFirm.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxlistfirm);
            listeducation.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxlisteducation);
            beginLearning.SelectedDate = Properties.Settings.Default.DatePickerBeginLearning;
            endLearning.SelectedDate = Properties.Settings.Default.DatePickerEndLearning;
            numberSertificate.Text = Properties.Settings.Default.TextBoxNumberSertificate;
            listLearningProgramm.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxListLearningProgram);
            try
            {
                foto.Source = new BitmapImage(new Uri(Properties.Settings.Default.Photo));

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Внимание! Требуется обязательно загрузить фото", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }     
        }

        public string FilePath = string.Empty;

        public void close_NewStudent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void select_newfirm(object sender, RoutedEventArgs e)
        {
            PassingText = listFirm.Text;
            NewFirm nf = new NewFirm();
            nf.ShowDialog();
            LoadComboBox();
        }
        private void force_number_sertificate_Click(object sender, RoutedEventArgs e)
        {
            SaveProperties();
            FormNumberSertificate fns = new FormNumberSertificate();
            fns.Show();
            Close();
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
        }
        private void insert_new_learning_program_Click(object sender, RoutedEventArgs e)
        {
            PassingText2 = listLearningProgramm.Text;
            NewLearningProgram nlp = new NewLearningProgram();
            nlp.ShowDialog();
            LoadComboBox();
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
            SaveProperties();
            Firm firm = (Firm)listFirm.SelectedItem;
            TrainingProgram trainingProg = (TrainingProgram)listLearningProgramm.SelectedItem;
            if (string.IsNullOrWhiteSpace(lastName.Text) || string.IsNullOrWhiteSpace(firstName.Text) || string.IsNullOrWhiteSpace(middleName.Text) || Birthday1.SelectedDate == null || listFirm.SelectedItem == null || string.IsNullOrWhiteSpace(position.Text) || listeducation.SelectedItem == null || string.IsNullOrWhiteSpace(numberdiploma.Text) || beginLearning.SelectedDate == null || endLearning.SelectedDate == null || string.IsNullOrWhiteSpace(numberSertificate.Text) || listLearningProgramm.SelectedItem == null)
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if(db.Student.Select(item=>item.Number_of_certificate).Contains(numberSertificate.Text))
            {
                MessageBoxBusy mbb = new MessageBoxBusy();
                mbb.Show();
                return;
            }
            else
            {
                Student NewStudent = new Student()
                {
                    LastName = lastName.Text,
                    FirstName = firstName.Text,
                    MiddleName = middleName.Text,
                    Birthday = Convert.ToDateTime(Birthday1.SelectedDate),
                    Firm = firm,
                    Post = position.Text,
                    NimberDiploma = numberdiploma.Text,
                    Education = listeducation.Text,
                    BeginLearning = Convert.ToDateTime(beginLearning.SelectedDate),
                    EndLearning = Convert.ToDateTime(endLearning.SelectedDate),
                    Number_of_certificate = numberSertificate.Text,
                    TrainingProgram = trainingProg,
                    Student_photo = ConvertImageToByteClass.ImageToByte(Properties.Settings.Default.Photo)
                };
                db.Student.Add(NewStudent);
                db.SaveChanges();
                MessageBoxInsert mbi = new MessageBoxInsert();
                mbi.Show();
            }
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            numberSertificate.Text = FormNumberSertificate.PassingText1;


        }
        private void SaveProperties()
        {
            Properties.Settings.Default.TextBoxlastname = lastName.Text;
            Properties.Settings.Default.TextBoxfirstname = firstName.Text;
            Properties.Settings.Default.TextBoxmiddlename = middleName.Text;
            Properties.Settings.Default.datepickerbirthday = Birthday1.SelectedDate.Value;
            Properties.Settings.Default.ComboBoxlistfirm = listFirm.SelectedIndex;
            Properties.Settings.Default.ComboBoxlisteducation = listeducation.SelectedIndex;
            Properties.Settings.Default.TextBoxposition = position.Text;
            Properties.Settings.Default.TextBoxnumberdiploma = numberdiploma.Text;
            Properties.Settings.Default.DatePickerBeginLearning = beginLearning.SelectedDate.Value;
            Properties.Settings.Default.DatePickerEndLearning = endLearning.SelectedDate.Value;
            Properties.Settings.Default.TextBoxNumberSertificate = numberSertificate.Text;
            Properties.Settings.Default.ComboBoxListLearningProgram = listLearningProgramm.SelectedIndex;
            if(foto.Source is BitmapImage bitmapImage && bitmapImage.UriSource != null)
            {
                Properties.Settings.Default.Photo = bitmapImage.UriSource.OriginalString;
            }
            else
            {
                Properties.Settings.Default.Photo = string.Empty;
            }
            
            Properties.Settings.Default.Save();
        }
        private void Fix_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Photo = FilePath;
            Properties.Settings.Default.Save();
        }
    }
}


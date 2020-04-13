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
    public partial class UpdateStudent : Window
    {
        TOSOT db;
        public string FilePath { get; private set; }

        public UpdateStudent(Student student)
        {
            InitializeComponent();
            db = new TOSOT();
            listFirm.ItemsSource = db.Firm.ToList();
            learningProgram.ItemsSource = db.TrainingProgram.ToList();
            DataContext = db.Student.Find(student.ID_Student);
        }
        public void Update_Click(object sender, RoutedEventArgs e)
        {
            if (lastName.Text == "" || firstName.Text == "" || middleName.Text == "" || Birthday.SelectedDate == null || listFirm.SelectedItem==null || position.Text == "" || listeducation.SelectedItem == null || numberdiploma.Text == "" || beginLearning.SelectedDate == null || endLearning.SelectedDate == null || numberSertificate.Text == "" || learningProgram.SelectedItem == null)
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            db.Entry(DataContext as Student).Property(x => x.Student_photo).CurrentValue = ConvertImageToByteClass.ImageToByte(FilePath);
            db.SaveChanges();
            MessageBoxChange mbch = new MessageBoxChange();
            mbch.Show();
            this.Close();
        }
        private void closeUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
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
    }
}

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
    public partial class NewLearningProgram : Window
    {
        TOSOT db;
        public NewLearningProgram()
        {
            InitializeComponent();
            db = new TOSOT();
        }
        private void closeNewLearningProgram_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void insert_new_learning_program_Click(object sender, RoutedEventArgs e)
        {
            if (NameLearningProgram.Text == "" || timelong.Text == "")
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.TrainingProgram.Select(item => item.Name_Program).Contains(NameLearningProgram.Text))
            {
                MessageBoxBusy mbb = new MessageBoxBusy();
                mbb.Show();
                return;
            }
            TrainingProgram NewTrainingProgram = new TrainingProgram()
            {
                Name_Program = NameLearningProgram.Text,
                Duration = Convert.ToInt32(timelong.Text)
            };
            db.TrainingProgram.Add(NewTrainingProgram);
            db.SaveChanges();
            this.Close();
            MessageBoxInsert mbi = new MessageBoxInsert();
            mbi.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameLearningProgram.Text = NewStudent.PassingText2;
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

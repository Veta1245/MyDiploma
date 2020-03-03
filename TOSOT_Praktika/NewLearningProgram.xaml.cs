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
    /// <summary>
    /// Логика взаимодействия для NewLearningProgram.xaml
    /// </summary>
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
            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 2;
            ns.Show();
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
            
            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 2;
            ns.Show();
            this.Close();
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

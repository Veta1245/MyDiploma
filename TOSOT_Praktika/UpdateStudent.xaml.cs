using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TOSOT_Praktika
{
    /// <summary>
    /// Логика взаимодействия для UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window
    {
        TOSOT db;

        public string FilePath { get; private set; }

        public UpdateStudent(Student student)
        {
            InitializeComponent();
            db = new TOSOT();
            
            DataContext = db.Student.Find(student.ID_Student);
            
            
        }
        
        
        
        public void Update_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
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

        
    }
}

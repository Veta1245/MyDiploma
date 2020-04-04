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
    public partial class MessageBoxVersion : Window
    {
       // Window creatingWindow;
        public MessageBoxVersion()
        {
            InitializeComponent();   
        }
        private void Yes_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void No_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                return;
            }      
        }
    }
}

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
    /// Логика взаимодействия для MessageBoxBusy.xaml
    /// </summary>
    public partial class MessageBoxBusy : Window
    {
        public MessageBoxBusy()
        {
            InitializeComponent();
        }

        private void busyfirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void closeMessageBoxBusy_Click(object sender, RoutedEventArgs e)
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

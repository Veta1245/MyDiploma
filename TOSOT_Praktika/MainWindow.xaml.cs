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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TOSOT_Praktika
{
    public partial class MainWindow : Window
    {
        MessageBoxVersion secondWindow;
        TOSOT db;
        public String LabelText { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            db = new TOSOT();
            MainPage mp = new MainPage();
            myFrame.NavigationService.Navigate(mp);
        }
        private void close_program(object sender, RoutedEventArgs e)
        {
            secondWindow = new MessageBoxVersion();
            secondWindow.Show();
        }
        private void Minimised (object sender, RoutedEventArgs e)
        {
             WindowState = WindowState.Minimized;
        }
        private void Maximized(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;       
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }    
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
           HelloyWorker.Content = LabelText;
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Show();
        }
    }
}

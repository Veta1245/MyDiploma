using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TOSOT_Praktika
{
    public partial class ProgressBarWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public ProgressBarWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            progressBar.Value++;
            if (progressBar.Value == 30)
            {
                timer.Stop();
                MessageBox.Show("Документы сформированы.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
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

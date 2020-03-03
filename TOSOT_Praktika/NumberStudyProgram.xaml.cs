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
    /// Логика взаимодействия для NumberStudyProgram.xaml
    /// </summary>
    public partial class NumberStudyProgram : Window
    {
        public static string PassingText;
        public NumberStudyProgram()
        {
            InitializeComponent();

        }

        private void closeNumderStudyProgram_Click(object sender, RoutedEventArgs e)
        {
            FormNumberSertificate fns = new FormNumberSertificate();
            fns.Show();
            this.Close();
        }

        private void choice_study_program1_Click(object sender, RoutedEventArgs e)
        {
            if (one.IsChecked == true)
            {
                reserve.Text = "01";
            }
            if (two.IsChecked == true)
            {
                reserve.Text = "02";
            }
            if (three.IsChecked == true)
            {
                reserve.Text = "03";
            }
            if (four.IsChecked == true)
            {
                reserve.Text = "04";
            }
            if (five.IsChecked == true)
            {
                reserve.Text = "05";
            }
            if (six.IsChecked == true)
            {
                reserve.Text = "06";
            }
            if (seven.IsChecked == true)
            {
                reserve.Text = "07";
            }
            if (eight.IsChecked == true)
            {
                reserve.Text = "08";
            }
            if (nine.IsChecked == true)
            {
                reserve.Text = "09";
            }
            if (ten.IsChecked == true)
            {
                reserve.Text = "10";
            }
            if (eleven.IsChecked == true)
            {
                reserve.Text = "11";
            }
            if (twelve.IsChecked == true)
            {
                reserve.Text = "12";
            }
            if (thirteen.IsChecked == true)
            {
                reserve.Text = "13";
            }
            if (fourteen.IsChecked == true)
            {
                reserve.Text = "14";
            }

            PassingText = reserve.Text;


            FormNumberSertificate fns = new FormNumberSertificate();
            fns.Show();
            this.Close();
               
            }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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


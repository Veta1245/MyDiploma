﻿using System;
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
    public partial class WindowStart : Window
    {
        public WindowStart()
        {
            InitializeComponent();
            PageAuthorisation pa = new PageAuthorisation();
            MyFrame.NavigationService.Navigate(pa);
        }     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void close_windowStart_Click(object sender, RoutedEventArgs e)
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

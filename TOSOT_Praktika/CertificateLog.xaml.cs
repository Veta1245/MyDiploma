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
    public partial class CertificateLog : Window
    {
        Model1Container1 db;
        public CertificateLog()
        {
            InitializeComponent();
            db = new Model1Container1();
            PageALLCertificateNumber pasn = new PageALLCertificateNumber();
            CertificateLogPage.NavigationService.Navigate(pasn);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void closeCertificateLog_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

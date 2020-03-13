﻿
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
    /// Логика взаимодействия для NewFirm.xaml
    /// </summary>
    public partial class NewFirm : Window
    {
        TOSOT db;
        public NewFirm()
        {
            InitializeComponent();
            db = new TOSOT();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameFirm.Text = NewStudent.PassingText;
        }

        private void closeNewFirm_Click(object sender, RoutedEventArgs e)
        {
            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 1;
            ns.Show();
            this.Close();
        }

        private void insert_new_firm_Click(object sender, RoutedEventArgs e)
        {
            if (NameFirm.Text == "")
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            if (db.Firm.Select(item => item.Name_Firm).Contains(NameFirm.Text))
            {
                MessageBoxBusy mbb = new MessageBoxBusy();
                mbb.Show();
                return;
            }
            Firm newfirm = new Firm()
            {
                Name_Firm = NameFirm.Text
            };
            db.Firm.Add(newfirm);
            db.SaveChanges();
            this.Close();
            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 1;
            ns.Show();
           
            
            MessageBoxInsert mbi = new MessageBoxInsert();
            mbi.Show();
            
            
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
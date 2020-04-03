﻿using System;
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
            if (lastName.Text == "" || firstName.Text == "" || middleName.Text == "" || Birthday.SelectedDate == null || firmName.Text == null || position.Text == "" || listeducation.SelectedItem == null || numberdiploma.Text == "" || beginLearning.SelectedDate == null || endLearning.SelectedDate == null || numberSertificate.Text == "" || trainingProgram.Text == null)
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            db.SaveChanges();
            MessageBoxChange mbch = new MessageBoxChange();
            mbch.Show();
            this.Close();
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
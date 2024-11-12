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
using System.Management;
using System.Data.SqlClient;
using TOSOT_Praktika.Properties;
using System.Configuration;
using System.IO;

namespace TOSOT_Praktika
{
    public partial class FormNumberSertificate : Window
    {
        Model1Container1 db = new Model1Container1();
        string text = string.Empty;
        public static string PassingText;
        public static string PassingText1;
        public FormNumberSertificate()
        {
            InitializeComponent();
            Load();
            ClearForm();
        }

        private void ClearForm()
        {
            yearLearning.Text = string.Empty;
            numberGroup.Text = string.Empty;
            listworker.SelectedItem = null; 
            studyProgram.Text = string.Empty;
        }

        void Load()
        {
            listworker.ItemsSource = db.Worker.ToList();
            yearLearning.Text = Properties.Settings.Default.TextBoxYearLearning;
            numberGroup.Text = Properties.Settings.Default.TextBoxNumberGroup;
            studyProgram.Text = Properties.Settings.Default.TextBoxStudyProgram;
            listworker.SelectedIndex = Convert.ToInt32(Properties.Settings.Default.ComboBoxListWorker);
        }

        private void closeFormNemderSertificate_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TextBoxYearLearning = yearLearning.Text;
            Properties.Settings.Default.TextBoxNumberGroup = numberGroup.Text;
            Properties.Settings.Default.TextBoxStudyProgram = studyProgram.Text;
            Properties.Settings.Default.ComboBoxListWorker = listworker.SelectedIndex;
            Properties.Settings.Default.Save();
            NewStudent ns = new NewStudent();
            ns.TabControlNewStudent.SelectedIndex = 2;
            ns.Show();
            this.Close();
        }
        private void choice_study_program_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TextBoxYearLearning = yearLearning.Text;
            Properties.Settings.Default.TextBoxNumberGroup = numberGroup.Text;
            Properties.Settings.Default.TextBoxStudyProgram = studyProgram.Text;
            Properties.Settings.Default.ComboBoxListWorker = listworker.SelectedIndex;
            Properties.Settings.Default.Save();
            NumberStudyProgram nsp = new NumberStudyProgram();
            nsp.Show();
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            studyProgram.Text = NumberStudyProgram.PassingText;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        public void collectNumber_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(yearLearning.Text) || string.IsNullOrWhiteSpace(numberGroup.Text) || string.IsNullOrWhiteSpace(studyProgram.Text))
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            var code = Code(studyProgram.Text);
            Worker worker = (Worker)listworker.SelectedItem;
            RegistrationCertificates certificate = new RegistrationCertificates()
            {
                Code = code,
                Worker = worker,
                DateCreate = DateTime.Now,
                ProtocolNumber = numberGroup.Text
            };
            db.Entry(certificate).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            sertificate.Text = code;
            PassingText1 = sertificate.Text;
            NewStudent Ns = new NewStudent();
            Ns.TabControlNewStudent.SelectedIndex = 2;
            Ns.Show();
            this.Close();
        }
        public string Code(string studyProgram)
        {
            string registText = string.Empty;
            string studyProgramID = $"ID{studyProgram}";
            try
            {
                foreach (var item in db.LearningProgram)
                {
                    if (item.KeyOfProgram == studyProgram)
                    {
                        if (DateTime.Now.Year == Properties.Settings.Default.DateNow)
                        {
                            if (Properties.Settings.Default[studyProgramID] != null)
                            {
                                int id;
                                int.TryParse(Properties.Settings.Default[studyProgramID].ToString(), out id);
                                id++;
                                Properties.Settings.Default[studyProgramID] = id.ToString();
                                string idProperty = String.Format("{0:d4}", id);
                                registText = $"{studyProgram}{yearLearning.Text}{numberGroup.Text}{idProperty}";
                                Properties.Settings.Default.Save();
                            }
                        }
                        else
                        {
                            Properties.Settings.Default.DateNow = DateTime.Now.Year;
                            for (int i = 1; i < db.LearningProgram.Count(); i++)
                            {
                                Properties.Settings.Default[String.Format("ID{0:d2}", i)] = "0";
                                Properties.Settings.Default.Save();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                text = "<setting name=\"" + studyProgramID + "\" serializeAs=\"String\"><value>0</value></setting>";
                string[] readText = System.IO.File.ReadAllLines(@"C:\Users\Veta\Desktop\TOSOT_Praktikaaaa\TOSOT_Praktika\App.config", Encoding.Default);
                readText[46] = readText[46] + Environment.NewLine + text;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Veta\Desktop\TOSOT_Praktikaaaa\TOSOT_Praktika\App.config", false))
                {
                    foreach (string s in readText)
                        file.WriteLine(s);
                }
            }
            return registText;
        }
    }
}

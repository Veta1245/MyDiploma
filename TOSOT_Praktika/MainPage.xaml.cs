using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
namespace TOSOT_Praktika
{
    public partial class MainPage : Page
    {
        TOSOT db;
        List<Student> students = new List<Student>();
        public string GroupNumber { get; set; }
        public MainPage()
        {
            InitializeComponent();
            db = new TOSOT();
            Load();
        }
        private void Load()
        {
            (ListStudent.Columns[5] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            (ListStudent.Columns[10] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            (ListStudent.Columns[11] as DataGridTextColumn).Binding.StringFormat = "dd:MM:yyyy";
            ListStudent.ItemsSource = db.Student.ToList();
            ListStudent.ScrollIntoView(ListStudent.Items[ListStudent.Items.Count - 1]);
        }
        private void insert_NewStudent(object sender, RoutedEventArgs e)
        {
            NewStudent ns = new NewStudent();
            ns.Show();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ListStudent.ItemsSource = db.Student.ToList().Where(x => x.Firm.Name_Firm.StartsWith(EntryField.Text) || x.LastName.StartsWith(EntryField.Text)).ToList();
        }
        private void CertificateLog_Click(object sender, RoutedEventArgs e)
        {
            CertificateLog cl = new CertificateLog();
            cl.Show();
        }
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (EntryField.Text == "")
            {
                MessageBoxEmpty mbe = new MessageBoxEmpty();
                mbe.Show();
                return;
            }
            foreach (Student item in ListStudent.SelectedItems)
            {
                students.Add(item);
            }
            try
            {
                Thread myThread = new Thread(new ThreadStart(Doc));
                myThread.Start();
                ProgressBarWindow progressBar = new ProgressBarWindow();
                progressBar.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Doc()
        {
            #region Первый документ
            Word.Application app1 = new Word.Application();
            Word.Document doc1 = app1.Documents.Add();
            for (int i = 0; i < 10; i++)
            {
                doc1.Paragraphs.Add();
            }
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var fullproject = Properties.Settings.Default.bootPath;
            string filePath = System.IO.Path.Combine(projectPath, "Resources/wordLogo.png");

            doc1.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc1.Paragraphs[2].Range.Font.Size = 14;
            doc1.Paragraphs[2].Range.Font.Color = Word.WdColor.wdColorDarkGreen;

            doc1.Paragraphs[4].Range.Font.Name = "Times New Roman";
            doc1.Paragraphs[4].Range.Font.Size = 14;
            doc1.Paragraphs[4].Range.Font.Bold = 1;

            doc1.Paragraphs[6].Range.Font.Name = "Times New Roman";
            doc1.Paragraphs[6].Range.Font.Size = 14;

            doc1.Paragraphs[7].Range.Font.Name = "Times New Roman";
            doc1.Paragraphs[7].Range.Font.Size = 14;

            doc1.Paragraphs[9].Range.Font.Name = "Times New Roman";
            doc1.Paragraphs[9].Range.Font.Size = 14;

            doc1.Paragraphs[10].Range.Font.Name = "Times New Roman";
            doc1.Paragraphs[10].Range.Font.Size = 14;

            doc1.Paragraphs[2].Range.Text = "Частное образовательное учреждение дополнительного профессионального образования “Томский областной центр охраны труда”";
            doc1.Paragraphs[2].Range.InlineShapes.AddPicture(filePath);
            doc1.Paragraphs[3].Range.Text = "\t\t\t\t\t\tПриказ";
            doc1.Paragraphs[4].Range.Text = $"{DateTime.Now.ToShortDateString()}г.\t\t\t\t\t\t\t\t\t№ ПБ {GroupNumber}/1\n";
            doc1.Paragraphs[5].Range.Text = $"О зачислении\n\tВ связи с началом занятий в группе №ПБ { GroupNumber}, обучающейся по программе «{ students.FirstOrDefault().TrainingProgram.Name_Program}», сроком обучения с { students.FirstOrDefault().BeginLearning.Value.ToShortDateString()}г. по { students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г. \nПРИКАЗЫВАЮ:\n1.Зачислить в состав группы № ПБ { GroupNumber} следующих слушателей:";
            doc1.Paragraphs[10].Range.Text = $"2. Назначить куратором группы № ПБ {GroupNumber} руководителя ОДО Румянцева А.Н.\n3.Назначить итоговую аттестацию на {students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г.";
            Word.Table table = doc1.Tables.Add(doc1.Paragraphs[9].Range, students.Count, 3);
            table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j = 0;

            table.Rows.Add();
            table.Cell(1, 1).Range.Text = "№ п/п";
            table.Cell(1, 2).Range.Text = "Ф.И.О.";
            table.Cell(1, 3).Range.Text = "Организация";
            int number = 1;
            for (int i = 0; i < students.Count; i++)
            {
                table.Cell(i+2, 1).Range.Text = number.ToString();
                table.Cell(i+2, 2).Range.Text = students[j].LastName + " " + students[j].FirstName + " " + students[j].MiddleName;
                table.Cell(i+2, 3).Range.Text = students[j].Firm.Name_Firm;
                j+=1;
                number += 1;
            }
            string path = fullproject;
            string subpath = GroupNumber;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
            doc1.SaveAs(fullproject + $"\\{GroupNumber}\\1 ПБ {GroupNumber}.1 Приказ о зачислении.docx");
            doc1.Close();
            app1.Quit();
            #endregion
            #region Второй документ
            Word.Application app2 = new Word.Application();
            Word.Document doc2 = app2.Documents.Add();
            for (int i = 0; i < 10; i++)
            {
                doc2.Paragraphs.Add();
            }
            doc2.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc2.Paragraphs[2].Range.Font.Size = 14;
            doc2.Paragraphs[2].Range.Font.Color = Word.WdColor.wdColorDarkGreen;

            doc2.Paragraphs[4].Range.Font.Name = "Times New Roman";
            doc2.Paragraphs[4].Range.Font.Size = 14;
            doc2.Paragraphs[4].Range.Font.Bold = 1;

            doc2.Paragraphs[6].Range.Font.Name = "Times New Roman";
            doc2.Paragraphs[6].Range.Font.Size = 14;

            doc2.Paragraphs[7].Range.Font.Name = "Times New Roman";
            doc2.Paragraphs[7].Range.Font.Size = 14;

            doc2.Paragraphs[9].Range.Font.Name = "Times New Roman";
            doc2.Paragraphs[9].Range.Font.Size = 14;

            doc2.Paragraphs[10].Range.Font.Name = "Times New Roman";
            doc2.Paragraphs[10].Range.Font.Size = 14;

            doc2.Paragraphs[2].Range.Text = "Частное образовательное учреждение дополнительного профессионального образования “Томский областной центр охраны труда”";
            doc2.Paragraphs[2].Range.InlineShapes.AddPicture(filePath);
            doc2.Paragraphs[3].Range.Text = "\t\t\t\t\t\tПриказ";
            doc2.Paragraphs[4].Range.Text = $"{students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г.\t\t\t\t\t\t\t\t\t№ ПБ {GroupNumber}/2\n";
            doc2.Paragraphs[5].Range.Text = $"О выпуске слушателей группы № ПБ {GroupNumber}\n\tВ связи с окончанием сроков обучения в группе №ПБ {GroupNumber}, обучающейся по программе, обучающейся по программе «{ students.FirstOrDefault().TrainingProgram.Name_Program}», сроком обучения с { students.FirstOrDefault().BeginLearning.Value.ToShortDateString()}г. по { students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г. \nПРИКАЗЫВАЮ:\n1.Отчислить из состава группы № ПБ { GroupNumber} слушателей, выполнивших учебный план и успешно прошедших итоговую аттестацию, в количестве {students.Count} человека";
            doc2.Paragraphs[10].Range.Text = $"2.Руководителю ОДО Румянцеву А.Н. организовать выдачу документов об окончании обучения.\nОснование: Протокол № ПБ {GroupNumber} от {students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г.";
            Word.Table table2 = doc2.Tables.Add(doc2.Paragraphs[9].Range, students.Count, 3);
            table2.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table2.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j2 = 0;
            table2.Rows.Add();
            table2.Cell(1, 1).Range.Text = "№ п/п";
            table2.Cell(1, 2).Range.Text = "Ф.И.О.";
            table2.Cell(1, 3).Range.Text = "Организация";
            int number2 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table2.Cell(i + 2, 1).Range.Text = number2.ToString();
                table2.Cell(i + 2, 2).Range.Text = students[j2].LastName + " " + students[j2].FirstName + " " + students[j2].MiddleName;
                table2.Cell(i + 2, 3).Range.Text = students[j2].Firm.Name_Firm;
                j2+=1;
                number2 += 1;
            }
            doc2.SaveAs(fullproject + $"\\{GroupNumber}\\2 ПБ {GroupNumber}.2 Приказ о выпуске.docx");
            doc2.Close();
            app2.Quit();
            #endregion
            #region Третий документ
            Word.Application app3 = new Word.Application();
            Word.Document doc3 = app3.Documents.Add();
            for (int i = 0; i < 2; i++)
            {
                doc3.Paragraphs.Add();
            }
            doc3.Paragraphs[1].Range.Font.Name = "Times New Roman";
            doc3.Paragraphs[1].Range.Font.Size = 14;
            doc3.Paragraphs[1].Range.Font.Bold = 1;

            doc3.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc3.Paragraphs[2].Range.Font.Size = 14;

            doc3.Paragraphs[1].Range.Text = $"\t\t\tСведения о слушателях группы № ПБ {GroupNumber}";
            Word.Table table3 = doc3.Tables.Add(doc3.Paragraphs[2].Range, students.Count, 6);
            table3.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table3.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j3 = 0;
            table3.Rows.Add();
            table3.Cell(1, 1).Range.Text = "№ п/п";
            table3.Cell(1, 2).Range.Text = "Фамилия, Имя, Отчество";
            table3.Cell(1, 3).Range.Text = "Должность";
            table3.Cell(1, 4).Range.Text = "Место работы";
            table3.Cell(1, 5).Range.Text = "Год рождения";
            table3.Cell(1, 6).Range.Text = "Образование";
            int number3 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table3.Cell(i + 2, 1).Range.Text = $"{number3}.";
                table3.Cell(i + 2, 2).Range.Text = students[j3].LastName + " " + students[j3].FirstName + " " + students[j3].MiddleName;
                table3.Cell(i + 2, 3).Range.Text = students[j3].Post;
                table3.Cell(i + 2, 4).Range.Text = students[j3].Firm.Name_Firm;
                table3.Cell(i + 2, 5).Range.Text = students[j3].Birthday.Value.ToShortDateString();
                table3.Cell(i + 2, 6).Range.Text = students[j3].Education;
                j3+=1;
                number3 += 1;
            }
            doc3.SaveAs(fullproject + $"\\{GroupNumber}\\3 Журнал.docx");
            doc3.Close();
            app3.Quit();
            #endregion
            #region Четвертый документ
            Word.Application app4 = new Word.Application();
            Word.Document doc4 = app4.Documents.Add();
            for (int i = 0; i < 2; i++)
            {
                doc4.Paragraphs.Add();
            }
            doc4.Paragraphs[1].Range.Font.Name = "Times New Roman";
            doc4.Paragraphs[1].Range.Font.Size = 14;
            doc4.Paragraphs[1].Range.Font.Bold = 1;

            doc4.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc4.Paragraphs[2].Range.Font.Size = 14;

            doc4.Paragraphs[1].Range.Text = $"\t\t\tВедомость учета итоговой аттестации";
            Word.Table table4 = doc4.Tables.Add(doc4.Paragraphs[2].Range, students.Count, 4);
            table4.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table4.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j4 = 0;
            table4.Rows.Add();
            table4.Cell(1, 1).Range.Text = "№";
            table4.Cell(1, 2).Range.Text = "Ф.И.О.";
            table4.Cell(1, 3).Range.Text = "Итоговый контроль";
            table4.Cell(1, 4).Range.Text = "Подпись";
            int number4 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table4.Cell(i + 2, 1).Range.Text = $"{number4}.";
                table4.Cell(i + 2, 2).Range.Text = students[j4].LastName + " " + students[j4].FirstName + " " + students[j4].MiddleName;
                table4.Cell(i + 2, 3).Range.Text = "зачтено";
                table4.Cell(i + 2, 4).Range.Text = string.Empty;
                j4+=1;
                number4 += 1;
            }
            doc4.SaveAs(fullproject + $"\\{GroupNumber}\\4 Ведомость итога экзамена.docx");
            doc4.Close();
            app4.Quit();
            #endregion
            #region Пятый документ
            Word.Application app5 = new Word.Application();
            Word.Document doc5 = app5.Documents.Add();
            for (int i = 0; i < 2; i++)
            {
                doc5.Paragraphs.Add();
            }
            doc5.Paragraphs[1].Range.Font.Name = "Times New Roman";
            doc5.Paragraphs[1].Range.Font.Size = 14;
            doc5.Paragraphs[1].Range.Font.Bold = 1;

            doc5.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc5.Paragraphs[2].Range.Font.Size = 14;

            doc5.Paragraphs[1].Range.Text = $"\t\t\tВедомость выдачи удостоверения";
            Word.Table table5 = doc5.Tables.Add(doc5.Paragraphs[2].Range, students.Count, 5);
            table5.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table5.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j5 = 0;
            table5.Rows.Add();
            table5.Cell(1, 1).Range.Text = "№";
            table5.Cell(1, 2).Range.Text = "Ф.И.О.";
            table5.Cell(1, 3).Range.Text = "Наименование предприятия";
            table5.Cell(1, 4).Range.Text = "Номер удостоверения";
            table5.Cell(1, 5).Range.Text = "Подпись";
            int number5 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table5.Cell(i + 2, 1).Range.Text = $"{number5}.";
                table5.Cell(i + 2, 2).Range.Text = students[j5].LastName + " " + students[j5].FirstName + " " + students[j5].MiddleName;
                table5.Cell(i + 2, 3).Range.Text = students[j5].Firm.Name_Firm;
                table5.Cell(i + 2, 4).Range.Text = students[j5].Number_of_certificate;
                table5.Cell(i + 2, 5).Range.Text = string.Empty;
                j5+=1;
                number5 += 1;
            }
            doc5.SaveAs(fullproject + $"\\{GroupNumber}\\5 Ведомость выдачи удостоверений.docx");
            doc5.Close();
            app5.Quit();
            #endregion
            #region Шестой документ
            Word.Application app6 = new Word.Application();
            Word.Document doc6 = app6.Documents.Add();
            for (int i = 0; i < 13; i++)
            {
                doc6.Paragraphs.Add();
            }
            doc6.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[2].Range.Font.Size = 14;

            doc6.Paragraphs[3].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[3].Range.Font.Size = 14;

            doc6.Paragraphs[4].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[4].Range.Font.Size = 14;
            doc6.Paragraphs[4].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[4].Range.Font.Size = 14;

            doc6.Paragraphs[6].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[6].Range.Font.Size = 14;

            doc6.Paragraphs[7].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[7].Range.Font.Size = 14;

            doc6.Paragraphs[9].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[9].Range.Font.Size = 14;

            doc6.Paragraphs[10].Range.Font.Name = "Times New Roman";
            doc6.Paragraphs[10].Range.Font.Size = 14;

            doc6.Paragraphs[2].Range.Text = $"\t\t\t\tПРОТОКОЛ   № ПБ {GroupNumber}\n\t\tзаседания комиссии по проверке знаний требований\n\t\t\t\tпромышленной безопасности";
            doc6.Paragraphs[5].Range.Text = $"\t\t\t\t\t\t\t\t\t{students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г.\n\tВ соответствии с приказом директора Частного образовательного учреждения дополнительного профессионального образования «Томский областной центр охраны труда» (ЧОУ ДПО «ТОЦОТ»)";
            doc6.Paragraphs[7].Range.Text = $"от {students.FirstOrDefault().BeginLearning.Value.ToShortDateString()}г. № ПБ {GroupNumber}/1. комиссия в составе:\nпредседателя: Красноженова С.П. – Директора ЧОУ ДПО «ТОЦОТ»;\nчленов: Шаминой В.Н. – Заместителя директора ЧОУ ДПО «ТОЦОТ»;\nРумянцева А.Н. – Руководителя ОДО ЧОУ ДПО «ТОЦОТ»;\nпровела проверку знаний по программе «{students.FirstOrDefault().TrainingProgram.Name_Program}»:";
            doc6.Paragraphs[13].Range.Text = $"Председатель комиссии:\t______________________\tС.П. Красноженов\nЧлены комиссии:\t\t\t______________________\tВ.Н.Шамина\n\t\t\t\t\t______________________ \tА.Н.Румянцев";
            Word.Table table6 = doc6.Tables.Add(doc6.Paragraphs[12].Range, students.Count, 5);
            table6.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table6.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j6 = 0;
            table6.Rows.Add();
            table6.Cell(1, 1).Range.Text = "№ п/п";
            table6.Cell(1, 2).Range.Text = "Ф.И.О.";
            table6.Cell(1, 3).Range.Text = "Наименование предприятия";
            table6.Cell(1, 4).Range.Text = "Номер удостоверения";
            table6.Cell(1, 5).Range.Text = "Подпись";
            int number6 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table6.Cell(i + 2, 1).Range.Text = number6.ToString();
                table6.Cell(i + 2, 2).Range.Text = students[j6].LastName + " " + students[j6].FirstName + " " + students[j6].MiddleName;
                table6.Cell(i + 2, 3).Range.Text = students[j6].Firm.Name_Firm;
                table6.Cell(i + 2, 4).Range.Text = students[j6].Number_of_certificate;
                table6.Cell(i + 2, 5).Range.Text = string.Empty;
                j6+=1;
                number6 += 1;
            }
            doc6.SaveAs(fullproject + $"\\{GroupNumber}\\6 Протокол ПБ {GroupNumber}.docx");
            doc6.Close();
            app6.Quit();
            #endregion
            #region Седьмой документ
            Word.Application app7 = new Word.Application();
            Word.Document doc7 = app7.Documents.Add();
            for (int i = 0; i < 2; i++)
            {
                doc7.Sections.Add();
            }
            for (int i = 0; i < 9; i++)
            {
                doc7.Paragraphs.Add();
            }
            doc7.Sections[1].Range.Font.Name = "Times New Roman";
            doc7.Sections[1].Range.Font.Size = 14;
            doc7.Paragraphs[1].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[1].Range.Font.Size = 14;
            doc7.Paragraphs[2].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[2].Range.Font.Size = 14;
            doc7.Paragraphs[3].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[3].Range.Font.Size = 14;
            doc7.Paragraphs[4].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[4].Range.Font.Size = 14;
            doc7.Paragraphs[5].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[5].Range.Font.Size = 14;
            doc7.Paragraphs[6].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[6].Range.Font.Size = 14;
            doc7.Paragraphs[7].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[7].Range.Font.Size = 14;
            doc7.Paragraphs[8].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[8].Range.Font.Size = 14;
            doc7.Paragraphs[9].Range.Font.Name = "Times New Roman";
            doc7.Paragraphs[9].Range.Font.Size = 14;
            app7.ActiveDocument.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = "Частное образовательное учреждение дополнительного профессионального образования “Томский областной центр охраны труда”";
            app7.ActiveDocument.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.InlineShapes.AddPicture(filePath);
            doc7.Paragraphs[1].Range.Text = $"\t\t\t\tПРОТОКОЛ № ПБ {GroupNumber}\n\t\tзаседания комиссии по проверке знаний требований\t\t\t\t\t\t\tпромышленной безопасности\n";
            doc7.Paragraphs[3].Range.Text = $"\t\t\t\t\t\t\t\t\t{students.FirstOrDefault().EndLearning.Value.ToShortDateString()}г.\n\tВ соответствии с приказом директора Частного образовательного учреждения дополнительного профессионального образования «Томский областной центр охраны труда» (ЧОУ ДПО «ТОЦОТ»)";
            doc7.Paragraphs[4].Range.Text = $"от {students.FirstOrDefault().BeginLearning.Value.ToShortDateString()}г. № ПБ {GroupNumber}/1 комиссия в составе:\nпредседателя: Красноженова С.П. – Директора ЧОУ ДПО «ТОЦОТ»;\nчленов: Шаминой В.Н. – Заместителя директора ЧОУ ДПО «ТОЦОТ»;\nРумянцева А.Н.  –  Руководителя ОДО ЧОУ ДПО «ТОЦОТ»;\nпровела проверку знаний по программе «{students.FirstOrDefault().TrainingProgram.Name_Program}»:";
            doc7.Paragraphs[9].Range.Text = $"Председатель комиссии:\t______________________\tС.П. Красноженов\nЧлены комиссии:\t\t\t______________________\tВ.Н.Шамина\n\t\t\t\t\t______________________ \tА.Н.Румянцев";
            Word.Table table7 = doc7.Tables.Add(doc7.Paragraphs[8].Range, students.Count, 5);
            table7.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table7.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j7 = 0;
            table7.Rows.Add();
            table7.Cell(1, 1).Range.Text = "№ п/п";
            table7.Cell(1, 2).Range.Text = "Ф.И.О.";
            table7.Cell(1, 3).Range.Text = "Наименование предприятия";
            table7.Cell(1, 4).Range.Text = "Номер удостоверения";
            table7.Cell(1, 5).Range.Text = "Подпись";
            int number7 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table7.Cell(i + 2, 1).Range.Text = number7.ToString();
                table7.Cell(i + 2, 2).Range.Text = students[j7].LastName + " " + students[j7].FirstName + " " + students[j7].MiddleName;
                table7.Cell(i + 2, 3).Range.Text = students[j7].Firm.Name_Firm;
                table7.Cell(i + 2, 4).Range.Text = students[j7].Number_of_certificate;
                table7.Cell(i + 2, 5).Range.Text = string.Empty;
                j7+=1;
                number7 += 1;
            }
            doc7.SaveAs(fullproject + $"\\{GroupNumber}\\7 Выписка Протокол ПБ {GroupNumber}.docx");
            doc7.Close();
            app7.Quit();
            #endregion
            #region Восьмой документ
            Word.Application app8 = new Word.Application();
            Word.Document doc8 = app8.Documents.Add();

            app8.ActiveDocument.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = "Лист ознакомления с инструкцией ИОТ-1/16 по проведению вводного инструктажа для обучающихся";

            Word.Table table8 = doc8.Tables.Add(doc8.Paragraphs[1].Range, students.Count, 3);
            table8.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table8.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int j8 = 0;
            table8.Rows.Add();
            table8.Cell(1, 1).Range.Text = "№ п/п";
            table8.Cell(1, 2).Range.Text = "Ф.И.О.";
            table8.Cell(1, 3).Range.Text = "Подпись";
            int number8 = 1;
            for (int i = 0; i < students.Count(); i++)
            {
                table8.Cell(i + 2, 1).Range.Text = number8.ToString();
                table8.Cell(i + 2, 2).Range.Text = students[j8].LastName + " " + students[j8].FirstName + " " + students[j8].MiddleName;
                table8.Cell(i + 2, 3).Range.Text = string.Empty;
                j8 += 1;
                number8 += 1;
            }
            doc8.SaveAs(fullproject + $"\\{GroupNumber}\\Вводный.docx");
            doc8.Close();
            app8.Quit();
            #endregion
            #region Девятый документ
            Word.Application app9 = new Word.Application();
            Word.Document doc9 = app9.Documents.Add();
            for (int i = 0; i < 39 * students.Count; i++)
            {
                doc9.Paragraphs.Add().Range.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            }

            int y = 0;
            foreach (var item in students)
            {
                doc9.Paragraphs[1 + y].Range.PageSetup.TextColumns.SetCount(2);
                doc9.Paragraphs[1 + y].Range.Font.Name = "Times New Roman";
                doc9.Paragraphs[1 + y].Range.Font.Size = 12;
                doc9.Paragraphs[1 + y].Range.Font.Bold = 1;
                doc9.Paragraphs[1 + y].Range.Text = "РОССИЙСКАЯ ФЕДЕРАЦИЯ\n" +
                    "Частное образовательное учреждение дополнительного профессионального образования «Томский областной центр охраны труда»\n" +
                    "УДОСТОВЕРЕНИЕ\n\n" +
                    "является документом о повышении\n" +
                    "квалификации\n" +
                    $"№ {item.Number_of_certificate}\n" +
                    $"Регистрационный номер № {item.ID_Student}\n\n\n\n\n\n\n" +
                    "Город\n" +
                    "Томск\n" +
                    $"Протокол № ПБ {GroupNumber} от {item.EndLearning.Value.ToShortDateString()} г.\n" +
                    "Дата выдачи\n" +
                    $"{item.EndLearning.Value.ToString("dd MMMM yyyy")} г.\n" +
                    $"Настоящее удостоверение свидетельствует о том, что\n" +
                    $"{item.LastName + " " + item.FirstName + " " + item.MiddleName}\n" +
                    $"Прошел (шла) обучение по дополнительной профессиональной программе\n" +
                    $"«{item.TrainingProgram.Name_Program}»\n\n\n\n\n\n" +
                    $"Удостоверение действительно в течение 5 лет\n\n\n\n\n\n\n\n" +
                    $"Председатель экзаменационной комиссии\nДиректор\t\t\t\t\tС.П.Красноженов\nСекретарь\t\t\t\t\tА.Н.Румянцев М.П.";

                doc9.Paragraphs[1 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[2 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[3 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[4 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[5 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[6 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[7 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[8 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[9 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[10 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[11 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[12 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[13 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[14 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[15 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[16 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[17 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[18 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[19 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[20 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[21 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[23 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc9.Paragraphs[29 + y].Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                y += 39;
            }
            doc9.SaveAs(fullproject + $"\\{GroupNumber}\\8 СВИДЕТЕЛЬСТВО.docx");
            doc9.Close();
            app9.Quit();
            #endregion
        }
        private void EntryField_SelectionChanged(object sender, RoutedEventArgs e)
        {
            GroupNumber = EntryField.Text;
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student student = ListStudent.SelectedItem as Student;
            UpdateStudent us = new UpdateStudent(student);
            us.ShowDialog();
            db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            ListStudent.ItemsSource = db.Student.ToList();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int num = ListStudent.Items.Count;
            coll.Content = num.ToString();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            Save();
        }
        private void Save()
        {
            ListStudent.ItemsSource = db.Student.ToList();
            int num = ListStudent.Items.Count;
            coll.Content = num.ToString();
        }
    }
}

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
using System.Threading;
using System.IO;
using Microsoft.Win32;
using TestingSystemConsole;

namespace TestingSystem
{
    struct Submit
    {
        static int counter = 0;
        /// <summary>
        /// Идентификатор отправленной задачи
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Статус решения программы. Формат passed/total
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Среднее время работы программы(в миллисекундах)
        /// </summary>
        public string AverageElapsedTime { get; set; }
        /// <summary>
        /// Количество используемой памяти(в мегабайтах)
        /// </summary>
        public string AverageMemoryUsage { get; set; }
        
        public Submit(string name, TestReport report)
        {
            ID = ++counter;
            UserName = name;
            // Считаем количество пройденных тестов
            // Считаем среднее время работы и среднее количество использованной памяти
            int passedTestCounter = 0;
            double averageElapsedTime = 0.0;
            double averageMemoryUsage = 0.0;
            for(int i = 0; i < report.results.Length; i++)
            {
                if(report.results[i].IsPassed)
                {
                    passedTestCounter++;
                }
                averageElapsedTime += report.results[i].AverageElapsedTime;
                averageMemoryUsage += report.results[i].MemoryUsage;
            }
            Status = passedTestCounter + "/" + report.results.Length;
            averageElapsedTime /= report.results.Length;
            averageMemoryUsage /= report.results.Length;
            AverageElapsedTime = string.Format("{0:#.##}мс", averageElapsedTime);
            AverageMemoryUsage = string.Format("{0:#.#}Мб", averageMemoryUsage/(1024*1024));
        }
        
    }
    public partial class MainWindow : Window
    {
        List<Submit> submits;

        string taskDir = @"C:\Projects\TestingSystem\bin\Debug\netcoreapp3.1\Tasks\Sorting\";
        public MainWindow()
        {
            InitializeComponent();

            submits = new List<Submit>();
            LeaderTable.ItemsSource = submits;

            // Получаем информацию о задаче Sorting
            TaskReader taskReader = new TaskReader(taskDir+"Task.txt");
            // Заполняем метки данными
            TaskName.Content = taskReader.TaskName;
            TaskDescription.Text = taskReader.TaskDescription;
            TaskInputExample.Text = taskReader.InputExample;
            TaskOutputExample.Text = taskReader.OutputExample;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            TestingStatus.Content = "";
            if(NameInput.Text == "")
            {
                MessageBox.Show("Имя пользователя не задано!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string pathToUserExecutable = openFileDialog.FileName;
                        // Начинаем тестирование пользовательской программы
                        Tester tester = new Tester(pathToUserExecutable, taskDir + "Tests.txt", taskDir + "Answers.txt", taskDir + "Restrictions.txt");
                        tester.Start();
                        tester.Close();
                        // Записываем результаты тестирования
                        submits.Add(new Submit(NameInput.Text, tester.Report));
                        TestingStatus.Content = "Тестирование окончено!";

                        string res = "";
                        for(int i = 0; i< submits.Count; i++)
                        {
                            res += submits[i].UserName + " ";
                        }
                        
                    }
                }
                
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
        }
        }
    }
}

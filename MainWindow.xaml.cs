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

using System.IO;

namespace TestingSystem
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Название текущей задачи
        /// </summary>
        private string currentTaskName = "Sorting";
        public MainWindow()
        {
            InitializeComponent();

            // Получаем информацию о задаче Sorting
            TaskReader taskReader = new TaskReader(@"Tasks/Sorting/Task.txt");
            // Заполняем метки данными
            TaskName.Content = taskReader.TaskName;
            TaskDescription.Text = taskReader.TaskDescription;
            TaskInputExample.Text = taskReader.InputExample;
            TaskOutputExample.Text = taskReader.OutputExample;
        }
    }
}
